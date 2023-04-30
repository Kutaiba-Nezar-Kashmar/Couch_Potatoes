import subprocess


def create_network(network_name: str, network_driver: str):
    command = ['docker', 'network', 'create',
               network_name, '--driver', network_driver]
    return subprocess.Popen(command, shell=False)


def compose_up(file: str, detached: bool, build: bool):
    command = ['docker', 'compose', '-f', file, 'up',
               '-d' if detached else None, '--build' if build else None]
    return subprocess.Popen(command, text=True, shell=False, stdout=subprocess.PIPE)


def compose_down(file: str):
    command = ['docker', 'compose', '-f', file, 'down']
    return subprocess.Popen(command, text=True, shell=False, stdout=subprocess.PIPE)
