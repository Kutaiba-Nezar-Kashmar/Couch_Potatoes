import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys"
import {getConfig} from "../configuration/configuration";
import Movie from "../models/movie";
import MovieRecommendations from "../models/movie-Recommedations";
import {getMovieCredits} from "./movie-credits";
import {getMovieDetails} from "./movie-details";

/**
 * Fetches all movie collections (popular, top rated, upcoming etc.)
 * When we destructure the data {isLoading, isError, data, error } = useFetchCollections(...),
 * then the we can access the different collections like in a dictionary: popular = data["popular"]
 * */

export function useFetchAllMovieCollections(options?: GetMovieCollectionOptions) {

    return useQuery({
        queryKey: [CacheKeys.ALL_COLLECTIONS + "" + options?.numberOfPages + options?.skipPages], queryFn: async () => {
            const popularMovies = await getMovieCollection("popular");
            const topRatedMovies = await getMovieCollection("top_rated");
            const nowPlayingMovies = await getMovieCollection("now_playing");

            return {
                popularMovies: popularMovies,
                topRatedMovies: topRatedMovies,
                NowPlayingMovies: nowPlayingMovies
            }
        }
    })
}

export function useFetchCollections(options?: GetMovieCollectionOptions) {
    return useQuery({
        queryKey: [CacheKeys.ALL_COLLECTIONS], queryFn: async (): Promise<object> => {
            return {
                popular: await getMovieCollection("popular", options)
            }
        }
    })
}

export function useFetchPopularMovies(options?: GetMovieCollectionOptions) {
    return useQuery({
        queryKey: [CacheKeys.POPULAR_MOVIES], queryFn: async () => {
            return await getMovieCollection("popular", options);
        }
    })
}

export function useFetchTopRatedMovies(options?: GetMovieCollectionOptions) {
    return useQuery({
        queryKey: [CacheKeys.TOP_RATED], queryFn: async () => {
            return await getMovieCollection("top_rated", options);
        }
    })
}

export interface GetMovieCollectionOptions {
    skipPages: number;
    numberOfPages: number;
}


async function getMovieCollection(collectionName: string, options?: GetMovieCollectionOptions): Promise<MovieRecommendations> {
    const {skipPages, numberOfPages} = options || {skipPages: 0, numberOfPages: 1};
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/movie-collection/${collectionName}?skip=${skipPages}&numberOfPages=${numberOfPages}`)
    if (!response.ok)
        throw new Error("Something went wrong");
    return response.json();
}

export async function getRecommendedMovies(movieId: number, options?: GetMovieCollectionOptions): Promise<MovieRecommendations> {
    const {skipPages, numberOfPages} = options || {skipPages: 0, numberOfPages: 1};
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/recommended-movies/${movieId}?skip=${skipPages}&numberOfPages=${numberOfPages}`)
    if (!response.ok)
        throw new Error("Something went wrong");
    return response.json();
}