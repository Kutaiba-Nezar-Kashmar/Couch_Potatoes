import PersonSearch from "./person-search";
import MovieSearch from "./movie-search";

export default interface MultiSearchResponse {
    people?: PersonSearch[];
    movies?: MovieSearch[];
}