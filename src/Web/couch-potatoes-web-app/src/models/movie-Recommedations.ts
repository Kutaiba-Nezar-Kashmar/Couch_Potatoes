
import Movie from "./movie";


export default interface MovieRecommendations {
    collection: Movie[];
    missingPages: number[];
}