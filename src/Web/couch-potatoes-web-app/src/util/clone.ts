export function slowClone<T>(obj: T): T {
    return JSON.parse(JSON.stringify(obj));
}
