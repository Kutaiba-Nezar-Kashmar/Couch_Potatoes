import { Express, Request, Response } from 'express';
import { IController } from './route';
import { IReviewRepository } from '../../repositories/index';
import { getReviewsForMovie } from '../../handlers/review/get-reviews';
import { match, P } from 'ts-pattern';
import { ResultType } from '../../common/monads';
import { Review } from '../../models/review';

export class ReviewsController implements IController {
    constructor(private repository: IReviewRepository) {
    }

    apply(server: Express): void {
        server.get('/api/v1/reviews/:movieId', async (req: Request, res: Response) => {
            const { movieId } = req.params;
            match(await getReviewsForMovie(Number(movieId), this.repository))
                .with({ type: ResultType.OK }, (reviews) => {
                    console.log("OK")
                    res.json(reviews.data);
                })
                .with({ type: ResultType.ERROR }, (err) => {
                    console.log('HERE 2');
                    res.status(404).json(err.error.message);
                })
                .with(P._, () => {
                    console.log('HERE');
                    res.statusMessage = 'Failed to get reviews';
                    res.status(500).end();
                })
                .exhaustive();
        });

        server.post('/api/v1/reviews/:movieId', async (req: Request, res: Response) => {
            const { movieId } = req.params;
            const review: Review = req.body;
            console.log(`Review: ${review}, id: ${movieId}`);
        });
    }
}
