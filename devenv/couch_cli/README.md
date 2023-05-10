# Couch CLI
A small convenience CLI tool for development purposes. 

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

