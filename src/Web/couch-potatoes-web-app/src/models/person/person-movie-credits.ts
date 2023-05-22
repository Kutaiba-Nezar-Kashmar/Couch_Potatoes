import Cast from "./cast";
import Crew from "./crew";

export default interface PersonMovieCredits {
    creditsAsCast: Cast[];
    creditsAsCrew: Crew[];
}