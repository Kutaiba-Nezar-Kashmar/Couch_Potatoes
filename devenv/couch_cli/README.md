# Couch CLI
A small convenience CLI tool for development purposes. 

# Compile a new version of the tool
Rust and cargo must be installed in order to compile new versions of this tool.

To build release version, run: 
```bash
$ cargo build --release
```

It will produce an artifact in ./target/release/couch_cli.exe

# Usage
The tool itself is stored in the root of the Couch Potates repository.

## Help
List of commands can be seen by running:
```bash
$ couch_cli --help
```

## Create new service
```bash
$ couch_cli new service MyService
```

## Start containers based on all docker compose files
The flags '-d' (detached) and '-b' (build) are optional. Consider always using '-d' and only using '-b' after making changes to a service.
```bash
$ couch_cli compose up -d -b
```

## Stop containers based on all docker compose files
```bash
$ couch_cli compose down
```

