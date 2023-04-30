from pathlib import Path
from sys import argv
from script_lib.file_search import recursive_search_for_files
from script_lib.docker_commands import create_network, compose_up


def main():
    project_root_path = Path(argv[1])
    print(f'Starting docker-compose search from: {str(project_root_path)}...')

    docker_compose_paths: list[Path] = []
    recursive_search_for_files(results=docker_compose_paths, current_path=project_root_path, file_names=[
                               'docker-compose.yaml', 'docker-compose.yml'], ignore_paths=['node_modules', '__pycache__'])

    print(f'\nFound {len(docker_compose_paths)} docker-compose files')
    for path in docker_compose_paths:
        print(str(path.absolute()))

    # NOTE: (mibui 2023-04-20) This will create an error response if the network already exists, but that is fine. We just want to ensure the network actually exists.
    print('\nCreating docker network: couch-potatoes-network')
    try:
        create_network(network_name='couch-potatoes-network',
                       network_driver='bridge').wait()
    except:
        pass

    print("\nStarting Docker composes...")
    for path in map(lambda x: str(x.absolute()), docker_compose_paths):
        process = compose_up(file=path, build=True, detached=True)
        out, err = process.communicate()
        print(f'stdout: {out}\nstderr: {err}')


main()
