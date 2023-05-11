use clap::Command;
use service::service_cmd;
use crate::compose::{compose_down_cmd, compose_up_cmd};

mod service;
mod compose;


fn cli() -> Command {
    Command::new("couch")
        .about("Couch Potatoes development CLI tool for more convenient development")
        .subcommand_required(true)
        .arg_required_else_help(true)
        .subcommand(
            Command::new("new")
                .about("Create new <service>")
                .subcommand_required(true)
                .arg_required_else_help(true)
                .subcommand(service_cmd()),
        )
        .subcommand(
            Command::new("compose")
                .about("Run compose up or compose down on all compose files in the project repository")
                .subcommand_required(true)
                .arg_required_else_help(true)
                .subcommand(compose_up_cmd())
                .subcommand(compose_down_cmd())
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
        Some(("compose", sub_matches)) => {
            let compose_command = sub_matches.subcommand().unwrap();
            match compose_command {
                ("up", sub_matches) => {
                    let detached = sub_matches.get_flag("detached");
                    let build = sub_matches.get_flag("build");
                    compose::start_all_composes(detached, build);
                }
                ("down", sub_matches) => {
                    compose::shut_down_all_composes();
                }
                _ => unreachable!()
            }
        }
        _ => unreachable!(),
    }
}
