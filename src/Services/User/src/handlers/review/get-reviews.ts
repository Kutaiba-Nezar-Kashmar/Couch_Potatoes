import { Result, ResultType } from '../../common/monads';
import { FailedToCreateReviewException } from '../../exceptions/failed-to-create-review';
import { Review } from '../../models/review';
import { IReviewRepository } from '../../repositories/review-repository';
import { match, P } from 'ts-pattern';
import { FailedToRetrieveReviewsException } from '../../exceptions/failed-to-retrieve-reviews';

export async function getReviewsForMovie(
    movieId: number,
    repository: IReviewRepository
): Promise<Result<Review[], FailedToRetrieveReviewsException>> {
    const reviews = await repository.getAllForMovie(movieId);
    return match(reviews)
        .with({type: ResultType.OK}, (r): Result<Review[], FailedToCreateReviewException>=> ({type: ResultType.OK, data: r.data}))
        .with(P._, (): Result<Review[], FailedToCreateReviewException> => ({type: ResultType.ERROR, error: new FailedToRetrieveReviewsException()}))
        .exhaustive()
}
