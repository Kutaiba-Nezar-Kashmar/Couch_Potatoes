import { User } from 'src/models/user';
import { IUserRepository } from 'src/repositories/user-repository';
import { OptionType, Result, ResultType } from 'src/common/monads';
import { match } from 'ts-pattern';
import { UserDoesNotExistException } from 'src/exceptions/user-does-not-exists';

export async function getUser(
    id: string,
    repository: IUserRepository
): Promise<Result<User, UserDoesNotExistException>> {
    const existingUser = match(await repository.get(id))
        .with({ type: OptionType.SOME }, (user) => ({
            type: ResultType.OK,
            data: user.data
        }))
        .with({ type: OptionType.NOTHING }, () => ({
            type: ResultType.ERROR,
            error: new UserDoesNotExistException(id)
        }))
        .run();

    return existingUser as Result<User, UserDoesNotExistException>;
}
