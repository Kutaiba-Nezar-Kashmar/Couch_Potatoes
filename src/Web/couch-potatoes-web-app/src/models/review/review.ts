import Movie from '../movie';
import User from '../user';
import { Vote } from './vote';

export interface Review {
    reviewId: string;
    user: User;
    movie: Movie;
    rating: number;
    reviewText: string;
    creationDate: string;
    lastUpdatedDate: string;
    votes: Vote[];
}
