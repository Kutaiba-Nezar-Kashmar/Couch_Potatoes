from pathlib import Path
from sys import argv
from script_lib.file_search import recursive_search_for_files
from script_lib.docker_commands import compose_down


def main():
    project_root_path = Path(argv[1])
    print(f'Starting docker-compose search from: {str(project_root_path)}...')

    docker_compose_paths: list[Path] = []
    recursive_search_for_files(results=docker_compose_paths, current_path=project_root_path, file_names=[
                               'docker-compose.yaml', 'docker-compose.yml'], ignore_paths=['node_modules', '__pycache__'])

    print(f'\nFound {len(docker_compose_paths)} docker-compose files')
    for path in docker_compose_paths:
        print(str(path.absolute()))

    print('\n')
    for path in map(lambda x: str(x.absolute()), docker_compose_paths):
        process = compose_down(file=path)
        out, err = process.communicate()
        print(f'stdout: {out}\nstderr: {err}')


main()
