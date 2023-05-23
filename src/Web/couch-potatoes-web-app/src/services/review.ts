import { useQuery } from 'react-query';
import { getConfig } from '../configuration/configuration';
import { Review } from '../models/review/review';
import { Vote } from '../models/review/vote';
import { getUserById, getUsers } from '../services/user';
import { getMovies } from './movie-details';
import Movie from '../models/movie';

export enum ReviewCacheKeys {
    REVIEWS_FOR_MOVIE = 'REVIEWS_FOR_MOVIE_',
    REVIEWS_FOR_USER = 'REVIEWS_FOR_USER_',
}

interface ReviewDto {
    reviewId: string;
    userId: string;
    movieId: number;
    rating: number;
    reviewText: string;
    creationDate: string;
    lastUpdatedDate: string;
    votes: Vote[];
}

export function useGetReviewsForMovie(movieId: number) {
    return useQuery({
        queryKey: [ReviewCacheKeys.REVIEWS_FOR_MOVIE + '' + movieId],
        queryFn: async () => {
            return await getReviewsForMovie(movieId);
        },
    });
}

export function useGetReviewsForUser(userId?: string) {
    return useQuery({
        queryKey: [ReviewCacheKeys.REVIEWS_FOR_USER + userId],
        queryFn: async () => {
            if (!userId) {
                return null;
            }

            return await getReviewsForUser(userId);
        },
    });
}

export async function getReviewsForMovie(movieId: number): Promise<Review[]> {
    const config = await getConfig();
    const reviewsResponse = await fetch(`${config.baseUrl}/reviews/${movieId}`);

    if (!reviewsResponse.ok) {
        throw new Error('Failed to fetch movies');
    }

    const reviewDtos: ReviewDto[] = await reviewsResponse.json();

    const userIds = reviewDtos.map((review) => review.userId);
    const users = await getUsers(userIds);
    const usersLookupMap = new Map(users.map((user) => [user.id, user]));

    const movieIds = reviewDtos.map((review) => review.movieId);
    const movies = await getMovies(movieIds);
    const moviesLookupMap = new Map<number, Movie>(
        movies.map((movie) => [movie.id, movie])
    );

    const reviews = reviewDtos.map((dto) => {
        return {
            creationDate: dto.creationDate,
            lastUpdatedDate: dto.lastUpdatedDate,
            movie: moviesLookupMap.get(dto.movieId)!,
            rating: dto.rating,
            reviewId: dto.reviewId,
            user: usersLookupMap.get(dto.userId)!,
            votes: dto.votes,
            reviewText: dto.reviewText,
        };
    });

    return reviews;
}

export async function getReviewsForUser(userId: string): Promise<Review[]> {
    const config = await getConfig();
    const reviewsResponse = await fetch(
        `${config.baseUrl}/users/${userId}/reviews`
    );

    if (!reviewsResponse.ok) {
        throw new Error('Failed to fetch movies');
    }

    const reviewDtos: ReviewDto[] = await reviewsResponse.json();

    const userIds = reviewDtos.map((review) => review.userId);
    const users = await getUsers(userIds);
    const usersLookupMap = new Map(users.map((user) => [user.id, user]));

    const movieIds = reviewDtos.map((review) => review.movieId);
    const movies = await getMovies(movieIds);
    const moviesLookupMap = new Map<number, Movie>(
        movies.map((movie) => [movie.id, movie])
    );

    const reviews = reviewDtos.map((dto) => {
        console.log(dto.votes);
        return {
            creationDate: dto.creationDate,
            lastUpdatedDate: dto.lastUpdatedDate,
            movie: moviesLookupMap.get(dto.movieId)!,
            rating: dto.rating,
            reviewId: dto.reviewId,
            user: usersLookupMap.get(dto.userId)!,
            votes: dto.votes,
            reviewText: dto.reviewText,
        };
    });

    return reviews;
}

export async function voteReview(
    userId: string,
    movieId: number,
    reviewId: string,
    direction: string
): Promise<Vote | null> {
    const config = await getConfig();
    const headers = {
        'Content-Type': 'application/json',
    };

    const payload = {
        userId: userId,
        direction: direction,
    };

    const reviewsResponse = await fetch(
        `${config.baseUrl}/reviews/${movieId}/${reviewId}/votes`,
        {
            headers: headers,
            body: JSON.stringify(payload),
            method: 'POST',
        }
    );

    if (!reviewsResponse.ok) {
        throw new Error('Failed to vote');
    }

    try {
        return await reviewsResponse.json();
    } catch (err) {
        return null;
    }
}

export async function updateReview(
    userId: string,
    movieId: number,
    reviewId: string,
    rating: number,
    reviewText: string
): Promise<ReviewDto> {
    const config = await getConfig();
    const headers = {
        'Content-Type': 'application/json',
    };

    const payload = {
        userId: userId,
        rating: rating,
        reviewText: reviewText,
    };

    const updateReviewResponse = await fetch(
        `${config.baseUrl}/reviews/${movieId}/${reviewId}`,
        {
            headers: headers,
            body: JSON.stringify(payload),
            method: 'PUT',
        }
    );

    if (!updateReviewResponse.ok) {
        throw new Error('Failed to update');
    }

    return await updateReviewResponse.json();
}

export async function deleteReview(
    movieId: number,
    reviewId: string,
    userId: string
): Promise<boolean> {
    const config = await getConfig();
    const headers = {
        'Content-Type': 'application/json',
    };

    const payload = {
        userId: userId,
    };

    const updateReviewResponse = await fetch(
        `${config.baseUrl}/reviews/${movieId}/${reviewId}`,
        {
            headers: headers,
            body: JSON.stringify(payload),
            method: 'DELETE',
        }
    );

    if (!updateReviewResponse.ok) {
        return false;
    }

    return true;
}

export async function createReview(
    movieId: number,
    userId: string,
    rating: number,
    reviewText: string
): Promise<Review> {
    const config = await getConfig();
    const headers = {
        'Content-Type': 'application/json',
    };

    const payload = {
        userId: userId,
        rating: rating,
        reviewText: reviewText,
    };

    const createReviewResponse = await fetch(
        `${config.baseUrl}/reviews/${movieId}`,
        {
            headers: headers,
            body: JSON.stringify(payload),
            method: 'POST',
        }
    );

    if (!createReviewResponse.ok) {
        throw new Error('Failed to create review');
    }

    const reviewDto: ReviewDto = await createReviewResponse.json();
    const user = await getUserById(userId);

    const movies = await getMovies([movieId]);

    return {
        creationDate: reviewDto.creationDate,
        lastUpdatedDate: reviewDto.lastUpdatedDate,
        movie: movies[0],
        rating: reviewDto.rating,
        reviewId: reviewDto.reviewId,
        user: user!,
        votes: reviewDto.votes,
        reviewText: reviewDto.reviewText,
    };
}
