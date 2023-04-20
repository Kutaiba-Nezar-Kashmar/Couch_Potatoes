from typing import Callable
from pathlib import Path, PurePath


def recursive_search_for_files(file_names: list[str], current_path: Path, results: list[Path], ignore_paths: list[str]) -> None:
    if PurePath(current_path).name in ignore_paths:
        return

    for entry in current_path.iterdir():
        if entry.is_file() and entry.name in file_names:
            results.append(entry)
        elif entry.is_dir():
            recursive_search_for_files(
                file_names=file_names, current_path=entry, results=results, ignore_paths=ignore_paths)
