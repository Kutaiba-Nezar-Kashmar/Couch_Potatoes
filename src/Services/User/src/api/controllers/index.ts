import { Express } from 'express';
import { ReviewsController } from './reviews';
import { FirebaseReviewRepository } from '../../repositories/review-repository';

export function applyControllers(server: Express) {
    const reviewRepository = new FirebaseReviewRepository();

    new ReviewsController(reviewRepository).apply(server);
}
