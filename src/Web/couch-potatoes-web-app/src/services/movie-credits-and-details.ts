import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys";
import {getMovieCredits} from "./movie-credits";
import {getMovieDetails} from "./movie-details";
import MovieCredits from "../models/movie_credits";
import MovieDetails from "../models/movie";

export interface MovieCreditsAndDetails {
    credits: MovieCredits;
    movieDetails: MovieDetails;
}


export function useFetchMovieCreditsAndMovies(movieId:number) {
    return useQuery({
        queryKey: [CacheKeys.MOVIE_CREDITS], queryFn: async () => {
            return {
                credits: await getMovieCredits(movieId),
                movieDetails: await getMovieDetails(movieId)
            }
        }
    })
}