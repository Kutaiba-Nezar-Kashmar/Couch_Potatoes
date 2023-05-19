import CastMember from "./cast_member";
import CrewMember from "./crew_member";


export default interface MovieCredits {
    id: number;
    creditsAsCast: CastMember[];
    creditsAsCrew: CrewMember[];
}