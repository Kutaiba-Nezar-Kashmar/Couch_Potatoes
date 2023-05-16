import { Option, Result } from 'src/common/monads';
import { User } from 'src/models/user';
export interface IUserRepository {
    create(user: User): Promise<Result<User, Error>>;
    get(id: string): Promise<Option<User>>;
}

export class FirebaseUserRepository implements IUserRepository {
    create(user: User): Promise<Result<User, Error>> {
        throw new Error('Not implemented yet');
    }
    get(id: string): Promise<Option<User>> {
        throw new Error('Not implemented yet');
    }
}
