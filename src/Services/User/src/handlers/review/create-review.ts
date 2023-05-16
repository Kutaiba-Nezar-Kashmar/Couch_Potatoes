import { Review } from '../../models/review';
import { Result, ResultType, voidData } from '../../common/monads';
import { IReviewRepository } from '../../repositories';
import { match, P } from 'ts-pattern';
import { FailedToCreateReviewException } from '../../exceptions/failed-to-create-review';

export async function createReview(review: Review, reviewRepository: IReviewRepository): Promise<Result<void, FailedToCreateReviewException>> {
    const userHasCreatedReviewForMovie = match(await reviewRepository.getAllForMovie(review.movieId))
        .with({ type: ResultType.OK }, (res) => res.data.some(review => review.userId === review.userId))
        .with({ type: ResultType.ERROR }, (res) => true)
        .with(P._, () => true)
        .exhaustive();

    if (userHasCreatedReviewForMovie) {
        return {
            type: ResultType.ERROR,
            error: new FailedToCreateReviewException(`User with id ${review.userId} has already created a review for movie ${review.movieId}`)
        };
    }

    if (!isValidReview(review)) {
        return {
            type: ResultType.ERROR,
            error: new FailedToCreateReviewException(`The review was invalid`)
        };
    }

    return {type: ResultType.OK, data: voidData()}
}

export function isValidReview(review: Review): boolean {
    const now = Date.now();
    return review.creationDate.getDate() < now
        && review.rating >= 0
        && review.userId !== null
        && review.userId !== undefined
        && review.movieId !== null
        && review.movieId !== undefined
        && !Number.isNaN(review.movieId);
}