export class UserDoesNotExistException extends Error {
    constructor(public id: string) {
        super(`User with id ${id} does not exist`);
    }
}
