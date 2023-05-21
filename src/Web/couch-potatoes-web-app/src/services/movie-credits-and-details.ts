import {useQuery} from "react-query";
import {CacheKeys} from "./cache-keys";
import {getMovieCredits} from "./movie-credits";
import {getMovieDetails} from "./movie-details";
import {getRecommendedMovies} from "./movie-collection";
import MovieCredits from "../models/movie_credits";
import MovieDetails from "../models/movie";
import MovieRecommendations from "../models/movie-Recommedations";
import Movie from "../models/movie";

export interface MovieCreditsAndDetails {
    credits: MovieCredits;
    movieDetails: MovieDetails;
    movieRecommendations: MovieRecommendations;
}


export function useFetchMovieCreditsAndMovies(movieId: number) {
    return useQuery({
        queryKey: [CacheKeys.MOVIE_CREDITS], queryFn: async () => {
            return {
                credits: await getMovieCredits(movieId),
                movieDetails: await getMovieDetails(movieId),
                movieRecommendations: await getRecommendedMovies(movieId)
            }
        }
    })
}