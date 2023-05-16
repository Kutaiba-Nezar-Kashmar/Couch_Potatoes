export class UserDoesNotExistException extends Error {
    constructor(public msg?: string) {
        super(msg);
    }
}
