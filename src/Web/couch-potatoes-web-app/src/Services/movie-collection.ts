import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys"
import {getConfig} from "../configuration/configuration";

export function useFetchPopularMovies(skipPage: number, numberOfPages: number) {
    return useQuery({
        queryKey: [CacheKeys.POPULAR_MOVIES], queryFn: async () => {
            const config = await getConfig();
            const response = await fetch(`${config.baseUrl}v1/movie-collection/popular?skip= ${skipPage}&numberOfPages=${numberOfPages}`)
            if (!response.ok)
                throw new Error("Something went wrong");
            return response.json();
        }
    })
}