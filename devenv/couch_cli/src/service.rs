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
    println!("Service to create: {name}");
    cd(&get_project_root_dir());
    cd("./src/Services/");
    mkdir(format!("./{name}").as_str());
    cd(format!("./{name}").as_str());

    let API_PROJECT_NAME = format!("{name}.API");
    let APPLICATION_PROJECT_NAME = format!("{name}.Application");
    let DOMAIN_PROJECT_NAME = format!("{name}.Domain");
    let INFRASTRUCTURE_PROJECT_NAME = format!("{name}.Infrastructure");

    println!("Creating new solution...");
    let mut output = create_solution(name);
    let solution_file = format!("{name}.sln");

    println!("Creating new API project...");
    output = create_dotnet_project("webapi", &API_PROJECT_NAME);
    print_output(output);

    println!("Creating new Application project...");
    output = create_dotnet_project("classlib", &APPLICATION_PROJECT_NAME);
    print_output(output);

    println!("Creating new Domain project...");
    output = create_dotnet_project("classlib", &DOMAIN_PROJECT_NAME);
    print_output(output);

    println!("Creating new Infrastricture project...");
    output = create_dotnet_project("console", &INFRASTRUCTURE_PROJECT_NAME);
    print_output(output);

    visit_service_project_folders_in_current_dir(name, post_service_project_creation);
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

fn mkdir(name: &str) {
    fs::create_dir(Path::new(name)).expect("Failed to create new dir");
}

fn create_solution(name: &str) -> Output {
    process::Command::new("dotnet")
        .args(["new", "sln", "--name", name])
        .output()
        .expect(format!("Failed to create new solution {name}").as_str())
}

fn create_dotnet_project(template: &str, name: &String) -> Output {
    process::Command::new("dotnet")
        .args(["new", template, "-o", format!("{name}").as_str()])
        .output()
        .expect(format!("Failed to create new Web API for service {name}").as_str())
}

fn add_project_to_solution(sln_file: &String, csproj_path: &String) -> Output {
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

/// on_visit takes a lambda with parameters: DirEntry and &str, where 1st &str is service_name and 2nd &str is the project folder name
fn visit_service_project_folders_in_current_dir(service_name: &String, on_visit: fn(&String, DirEntry, &String)) {
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

                if isDir && file_name.contains(service_name) {
                    on_visit(service_name, dir_entry, &file_name);
                }
            }
            Err(_) => exit(1),
        }
    }
}

fn add_project_reference(source_project_csproj_path: &str, dependency_project_csproj_path: &str) -> Output {
    process::Command::new("dotnet")
        .args(["add", source_project_csproj_path, "reference", dependency_project_csproj_path])
        .output()
        .expect("Failed to add reference")
}

fn add_nuget_package_to_project(csproj_path: &str, package_name: &str) -> Output {
    process::Command::new("dotnet")
        .args(["add", csproj_path, "package", package_name])
        .output()
        .expect("Failed to add dependency")
}

fn post_service_project_creation(service_name: &String, entry_dir: fs::DirEntry, file_name: &String) {
    let API_PROJECT_NAME = format!("{service_name}.API");
    let APPLICATION_PROJECT_NAME = format!("{service_name}.Application");
    let DOMAIN_PROJECT_NAME = format!("{service_name}.Domain");
    let INFRASTRUCTURE_PROJECT_NAME = format!("{service_name}.Infrastructure");

    let solution_file = format!("{service_name}.sln");
    let api_csproj = format!("{API_PROJECT_NAME}/{API_PROJECT_NAME}.csproj");
    let application_csproj = format!("{APPLICATION_PROJECT_NAME}/{APPLICATION_PROJECT_NAME}.csproj");
    let domain_csproj = format!("{DOMAIN_PROJECT_NAME}/{DOMAIN_PROJECT_NAME}.csproj");
    let infrastructure_csproj = format!("{INFRASTRUCTURE_PROJECT_NAME}/{INFRASTRUCTURE_PROJECT_NAME}.csproj");

    let csproj_path = format!("./{file_name}/{file_name}.csproj");
    println!("Adding {csproj_path} to solution...");
    let mut output = add_project_to_solution(&solution_file, &csproj_path);
    print_output(output);

    match file_name.to_string() {
        project if project.contains(".API") => {
            println!("Setting up references and installing packages for: {file_name}");
            output = add_project_reference(csproj_path.as_str(), &application_csproj);
            print_output(output);

            output = add_project_reference(csproj_path.as_str(), &domain_csproj);
            print_output(output);

            output = add_project_reference(csproj_path.as_str(), &infrastructure_csproj);
            print_output(output);

            output = add_nuget_package_to_project(csproj_path.as_str(), "MediatR");
            print_output(output);
        }
        project if project.contains(".Application") => {
            output = add_project_reference(csproj_path.as_str(), &domain_csproj);
            print_output(output);

            output = add_project_reference(csproj_path.as_str(), &infrastructure_csproj);
            print_output(output);

            output = add_nuget_package_to_project(csproj_path.as_str(), "MediatR");
            print_output(output);
        }
        project if project.contains(".Infrastructure") => {
            output = add_project_reference(csproj_path.as_str(), &domain_csproj);
            print_output(output);

            output = add_nuget_package_to_project(csproj_path.as_str(), "AutoMapper");
            print_output(output);
        }
        _ => {}
    }
}