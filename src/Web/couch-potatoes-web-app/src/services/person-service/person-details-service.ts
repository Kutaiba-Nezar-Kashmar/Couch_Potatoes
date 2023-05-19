import {useQuery} from "react-query";
import {CacheKeys} from "../cache-keys";
import PersonDetails from "../../models/person/person-details";
import PersonMovieCredits from "../../models/person/person-movie-credits";

export function useFetchPersonDetailsAndCredits(options: FetchPersonDetailsOptions) {
    return useQuery({
        queryKey: [CacheKeys.PERSON_MOVIE_DETAILS_AND_CREDITS], queryFn: async () =>{
            return {
                details: await fetchPersonDetails(options),
                credits: await fetchPersonMovieCredits(options)
            }
        }
    })
}

/*export function useFetchPersonDetails(options: FetchPersonDetailsOptions) {
    return useQuery({
        queryKey: [CacheKeys.PERSON_DETAILS + options.personId], queryFn: async (): Promise<object> => {
            return await fetchPersonDetails(options)
        }
    })
}

export function useFetchPersonMovieCredits(options: FetchPersonDetailsOptions) {
    return useQuery({
        queryKey: [CacheKeys.PERSON_MOVIE_CREDITS + options.personId], queryFn: async (): Promise<object> => {
            return await fetchPersonMovieCredits(options);
        }
    })
}*/

export interface FetchPersonDetailsOptions {
    personId: number
}

// TODO: remember to fetch the base uri later from configs when we setup the gateway
async function fetchPersonDetails(options: FetchPersonDetailsOptions): Promise<PersonDetails> {
    const {personId} = options;
    const response = await fetch(`http://localhost:8084/couch-potatoes/api/v1/person/details/${personId}`);
    if (!response.ok) throw new Error("Cannot fetch person details");
    return response.json();
}

async function fetchPersonMovieCredits(options: FetchPersonDetailsOptions): Promise<PersonMovieCredits> {
    const {personId} = options;
    const response = await fetch(`http://localhost:8084/couch-potatoes/api/v1/person/movie-credits/${personId}`);
    if (!response.ok) throw new Error("Cannot fetch person movie credits");
    return response.json();
}