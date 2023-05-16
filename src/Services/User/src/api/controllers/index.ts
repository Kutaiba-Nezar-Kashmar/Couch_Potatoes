import { Express } from 'express';
import { ReviewsController } from './reviews';
import { FirebaseReviewRepository } from '../../repositories/review-repository';

export default function apply(server: Express) {
    const reviewRepository = new FirebaseReviewRepository();

    new ReviewsController(reviewRepository).apply(server);
}
