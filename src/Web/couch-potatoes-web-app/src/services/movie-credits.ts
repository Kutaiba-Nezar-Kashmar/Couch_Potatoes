import {getConfig} from "../configuration/configuration";
import Movie from "../models/movie";
import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys";
import {GetMovieCollectionOptions} from "./movie-collection";
import MovieCredits from "../models/movie_credits";


export function useFetchMovieCredits(movieId:number) {
    return useQuery({
        queryKey: [CacheKeys.MOVIE_CREDITS], queryFn: async () => {
            return await getMovieCredits(movieId);
        }
    })
}


export async function getMovieCredits(movieId: number): Promise<MovieCredits> {
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/movie-credits/${movieId}`)
    if (!response.ok)
        throw new Error("Something went wrong");
    return response.json();
}