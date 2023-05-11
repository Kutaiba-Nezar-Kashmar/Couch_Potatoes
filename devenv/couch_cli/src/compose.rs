use clap::{Command, Arg, ArgAction};
use std::{env, fs::{self, DirEntry}, io, path::Path, process::{self, exit, Output}};
use std::io::{BufRead, BufReader, Read};
use std::process::{Child, Stdio};

pub fn compose_up_cmd() -> Command {
    Command::new("up")
        .arg(
            Arg::new("detached")
                .short('d')
                .long("detached")
                .action(ArgAction::SetTrue)
        )
        .arg(
            Arg::new("build")
                .short('b')
                .long("build")
                .action(ArgAction::SetTrue)
        )
}

pub fn compose_down_cmd() -> Command {
    Command::new("down")
}


pub fn start_all_composes(detached: bool, build: bool) {
    let project_root_dir = get_project_root_dir();
    cd(&project_root_dir);
    println!("Searching for docker-compose files from: {}", &project_root_dir);

    let ignore_paths = vec!["node_modules", "__pycache__", "couch_cli", ""].iter()
        .map(|s| s.to_string())
        .collect();

    let network_name = "couch-potatoes-network";
    let driver = "bridge";
    println!("Creating docker network {network_name} with driver {driver}");
    let output = create_docker_network(network_name, driver);
    print_output(output);


    let compose_files = get_compose_files(&project_root_dir, &ignore_paths);
    println!("Found {} docker-compose files", compose_files.len());
    compose_files.iter().for_each(|f| {
        println!("Running docker compose up on {f}");
        println!("This may take a while ");
        let mut command = docker_compose_up(f, true, true);

        let stdout = command.stdout.as_mut().unwrap();

        let stdout_reader = BufReader::new(stdout);
        let lines = stdout_reader.lines();

        for line in lines {
            println!("{:?}", line);
        }
    })
}

pub fn shut_down_all_composes() {
    let project_root_dir = get_project_root_dir();
    println!("Searching for docker-compose files from: {}", &project_root_dir);
    cd(&project_root_dir);

    let ignore_paths = vec!["node_modules", "__pycache__", "couch_cli", ""].iter()
        .map(|s| s.to_string())
        .collect();

    let compose_files = get_compose_files(&project_root_dir, &ignore_paths);
    println!("Found {} docker-compose files", compose_files.len());
    compose_files.iter().for_each(|f| {
        let output = docker_compose_down(&f);
        print_output(output)
    })
}


fn create_docker_network(name: &str, driver: &str) -> Output {
    process::Command::new("docker")
        .args(["network", "create", name, "--driver", driver])
        .output()
        .expect("Failed to execute docker create network")
}

fn docker_compose_up(compose_file_path: &str, detached: bool, build: bool) -> Child {
    let args = match (detached, build) {
        (false, false) => vec!["compose", "-f", compose_file_path, "up"],
        (true, false) => vec!["compose", "-f", compose_file_path, "up", "-d"],
        (false, true) => vec!["compose", "-f", compose_file_path, "up", "--build"],
        (true, true) => vec!["compose", "-f", compose_file_path, "up", "-d", "--build"],
        _ => unreachable!()
    };

    process::Command::new("docker".to_string())
        .args(args)
        .stdout(Stdio::piped())
        .spawn()
        .unwrap()
}

fn docker_compose_down(compose_file_path: &str) -> Output {
    process::Command::new("docker")
        .args(["compose", "-f", compose_file_path, "down"])
        .output()
        .expect("Failed to run docker compose down")
}


fn get_compose_files(dir: &str, ignore_paths: &Vec<String>) -> Vec<String> {
    let mut compose_files = Vec::<String>::new();

    let mut visit = |entry: DirEntry| add_to_list_if_compose(entry.path().to_str().unwrap(), &mut compose_files);
    visit_dirs(Path::new(dir), ignore_paths, &mut visit).expect("Failed to get compose files");

    compose_files
}

fn add_to_list_if_compose(file_path: &str, list: &mut Vec<String>) {
    if !is_compose_file(file_path) {
        return;
    }

    list.push(file_path.to_string());
}

fn is_compose_file(file_path: &str) -> bool {
    file_path.contains("docker-compose.yaml") || file_path.contains("docker-compose.yml")
}


fn visit_dirs(dir: &Path, ignore_paths: &Vec<String>, on_visit: &mut impl FnMut(DirEntry)) -> io::Result<()> {
    if dir.is_dir() {
        for entry in fs::read_dir(dir)? {
            let entry = entry?;
            let path = entry.path();

            if ignore_paths.iter().any(|s| s == entry.file_name().to_str().unwrap()) {
                return Ok(());
            }

            if path.is_dir() {
                visit_dirs(&path, &ignore_paths, on_visit)?;
            } else {
                on_visit(entry);
            }
        }
    }
    Ok(())
}

fn get_project_root_dir() -> String {
    let project_name = "Couch_Potatoes";
    let mut current_dir = get_current_dir();
    let path_parts = current_dir.split("\\");
    let path_parts = path_parts.collect::<Vec<&str>>();

    let already_at_project_root = match path_parts.get(path_parts.len() - 1) {
        Some(name) => *name == project_name,
        None => false,
    };

    if already_at_project_root {
        return current_dir;
    }

    if path_parts.into_iter().any(|p| p == project_name) {
        let mut at_project_root = false;
        while !at_project_root {
            cd("..");
            current_dir = get_current_dir();
            let path_parts = current_dir.split("\\");
            let path_parts = path_parts.collect::<Vec<&str>>();
            at_project_root = match path_parts.get(path_parts.len() - 1) {
                Some(name) => *name == project_name,
                None => false,
            };
        }
        return current_dir;
    } else {
        ""
    };

    return get_current_dir();
}

fn get_current_dir() -> String {
    String::from(env::current_dir().unwrap().as_os_str().to_str().unwrap())
}

fn cd(path: &str) {
    env::set_current_dir(Path::new(path)).expect("Failed to change dir");
}

fn print_output(output: Output) {
    println!("stdout: {}", String::from_utf8_lossy(&output.stdout));
    println!("stderr: {}", String::from_utf8_lossy(&output.stderr));
}
