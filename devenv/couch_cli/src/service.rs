use clap::{arg, Arg, Command};
use std::{
    env,
    fs::{self, DirEntry},
    path::Path,
    process::{self, exit, Output},
};

pub fn service_cmd() -> Command {
    Command::new("service")
        .about("Creates a new service")
        .arg(Arg::new("name"))
        .arg_required_else_help(true)
}

pub fn create_new_service(name: &String) {
    let service_path = Path::new("./src/Services/");
    println!("Creating new service: {name}");
    cd(Path::new(&get_project_root_dir()));
    cd(service_path);
    fs::create_dir(format!("./{name}")).expect("Failed to create dir");
    cd(Path::new(format!("./{name}").as_str()));

    println!("Creating new solution...");
    let mut output = create_solution(name);
    let solution_file = format!("{name}.sln");

    println!("Creating new API project...");
    output = create_dotnet_project("webapi", format!("{name}.API").as_str());
    print_output(output);

    println!("Creating new Application project...");
    output = create_dotnet_project("classlib", format!("{name}.Application").as_str());
    print_output(output);

    println!("Creating new Domain project...");
    output = create_dotnet_project("classlib", format!("{name}.Domain").as_str());
    print_output(output);

    println!("Creating new Infrastricture project...");
    output = create_dotnet_project("console", format!("{name}.Infrastructure").as_str());
    print_output(output);

    let paths = fs::read_dir("./").unwrap().into_iter();

    for path in paths {
        match path {
            Ok(dir_entry) => {
                let isDir = match dir_entry.file_type().as_ref() {
                    Ok(file) => file.is_dir(),
                    Err(_) => false,
                };

                let file_name = String::from(
                    dir_entry
                        .file_name()
                        .to_str()
                        .expect("Failed to read file name"),
                );
                if isDir && file_name.contains(name) {
                    let csproj_path = format!("./{file_name}/{file_name}.csproj");
                    println!("{csproj_path}");
                    println!("Adding {csproj_path} to solution...");
                    output = add_project_to_solution(&solution_file, &csproj_path);
                    print_output(output);
                }
            }
            Err(_) => exit(1),
        }
    }
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
            cd(Path::new(".."));
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

fn cd(path: &Path) {
    env::set_current_dir(path).expect("Failed to change dir");
}

fn create_solution(name: &str) -> Output {
    process::Command::new("dotnet")
        .args(["new", "sln", "--name", name])
        .output()
        .expect(format!("Failed to create new solution {name}").as_str())
}

fn create_dotnet_project(template: &str, name: &str) -> Output {
    process::Command::new("dotnet")
        .args(["new", template, "-o", format!("{name}").as_str()])
        .output()
        .expect(format!("Failed to create new Web API for service {name}").as_str())
}

fn add_project_to_solution(sln_file: &str, csproj_path: &str) -> Output {
    process::Command::new("dotnet")
        .args([
            "sln",
            format!("{sln_file}").as_str(),
            "add",
            format!("{csproj_path}").as_str(),
        ])
        .output()
        .expect("Failed to add project to solution")
}

fn print_output(output: Output) {
    println!("stdout: {}", String::from_utf8_lossy(&output.stdout));
    println!("stderr: {}", String::from_utf8_lossy(&output.stderr));
}
