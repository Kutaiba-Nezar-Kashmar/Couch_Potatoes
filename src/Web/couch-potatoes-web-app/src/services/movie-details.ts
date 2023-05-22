import { getConfig } from '../configuration/configuration';
import Movie from '../models/movie';
import { useQuery } from 'react-query';
import { CacheKeys } from './cache-keys';
import { GetMovieCollectionOptions } from './movie-collection';

export function useFetchMovies(movieIds: number[]) {
    return useQuery({
        queryKey: [CacheKeys.MOVIES + JSON.stringify(movieIds)],
        queryFn: async () => {
            return await getMovies(movieIds);
        },
    });
}

export function useFetchMovieDetails(movieId: number) {
    return useQuery({
        queryKey: [CacheKeys.CURRENT_MOVIE],
        queryFn: async () => {
            return await getMovieDetails(movieId);
        },
    });
}

export async function getMovieDetails(movieId: number): Promise<Movie> {
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/movie-details/${movieId}`);
    if (!response.ok) throw new Error('Something went wrong');
    return response.json();
}

export async function getMovies(movieIds: number[]): Promise<Movie[]> {
    const config = await getConfig();
    const queryParams = movieIds.map((id) => `ids=${id}`).join('&');

    const response = await fetch(
        `${config.baseUrl}/movie-details?${queryParams}`
    );

    if (!response.ok) {
        throw new Error('Failed to fetch movies');
    }

    return response.json();
}
