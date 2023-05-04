use clap::Command;
use service::service_cmd;
mod service;

fn cli() -> Command {
    Command::new("couch")
        .about("Couch Potatoes development CLI")
        .subcommand_required(true)
        .arg_required_else_help(true)
        .subcommand(
            Command::new("new")
                .about("Create new ...")
                .subcommand_required(true)
                .arg_required_else_help(true)
                .subcommand(service_cmd()),
        )
}

fn main() {
    let matches = cli().get_matches();

    match matches.subcommand() {
        Some(("new", sub_matches)) => {
            let new_command = sub_matches.subcommand().unwrap();
            match new_command {
                ("service", sub_matches) => {
                    let service_name = sub_matches.get_one::<String>("name");
                    service::create_new_service(service_name.unwrap());
                }
                _ => unreachable!(),
            }
        }
        _ => unreachable!(),
    }
}
