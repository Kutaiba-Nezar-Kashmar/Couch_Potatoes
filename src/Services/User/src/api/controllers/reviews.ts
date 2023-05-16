import { Express, Request, Response } from 'express';
import { IController } from './route';
import { IReviewRepository } from '../../repositories/index';
import { getReviewsForMovie } from '../../handlers/review/get-reviews';
import { match } from 'ts-pattern';
import { ResultType } from '../../common/monads';

export class ReviewsController implements IController {
    constructor(private repository: IReviewRepository) {}

    apply(server: Express): void {
        server.get('reviews/:movieId', async (req: Request, res: Response) => {
            const { movieId } = req.params;
            match(await getReviewsForMovie(Number(movieId), this.repository))
                .with({ type: ResultType.OK }, (reviews) => res.json(reviews.data))
                .with({ type: ResultType.ERROR }, (err) => res.json(err)).exhaustive;
        });
    }
}
