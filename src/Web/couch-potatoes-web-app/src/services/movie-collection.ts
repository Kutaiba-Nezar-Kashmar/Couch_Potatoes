import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys"
import {getConfig} from "../configuration/configuration";
import Movie from "../models/movie";

/**
 * Fetches all movie collections (popular, top rated, upcoming etc.)
 * When we destructure the data {isLoading, isError, data, error } = useFetchCollections(...),
 * then the we can access the different collections like in a dictionary: popular = data["popular"]
 * */
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

async function getMovieCollection(collectionName: string, options?: GetMovieCollectionOptions): Promise<Movie[]> {
    const {skipPages, numberOfPages} = options || {skipPages: 0, numberOfPages: 1};
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/movie-collection/${collectionName}?skip=${skipPages}&numberOfPages=${numberOfPages}`)
    if (!response.ok)
        throw new Error("Something went wrong");
    return response.json();
}