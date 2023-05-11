import {useQuery} from "react-query";
import config from "../couch-config.json";
import {CacheKeys} from "./cache-keys"

export function useFetchPopularMovies(skipPage: number, numberOfPages: number) {
    return useQuery({
        queryKey: [CacheKeys.POPULAR_MOVIES], queryFn: async () => {
            const response = await fetch(`${config.baseUrl}v1/movie-collection/popular?skip= ${skipPage}&numberOfPages=${numberOfPages}`)
            if (!response.ok)
                throw new Error("Something went wrong");
            return response.json();
        }
    })
}