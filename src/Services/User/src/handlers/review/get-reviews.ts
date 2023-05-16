import { Result, ResultType } from '../../common/monads';
import { FailedToCreateReviewException } from '../../exceptions/failed-to-create-review';
import { Review } from '../../models/review';
import { IReviewRepository } from '../../repositories/review-repository';
import { match } from 'ts-pattern';

export async function getReviewsForMovie(
    movieId: number,
    repository: IReviewRepository
): Promise<Result<Review[], FailedToCreateReviewException>> {
    return match(await repository.getAllForMovie(movieId))
        .with({ type: ResultType.OK }, (res) => res)
        .with({ type: ResultType.ERROR }, (res) => ({
            type: res.type,
            error: new FailedToCreateReviewException('Failed to create review exception'),
        })).exhaustive;
}
