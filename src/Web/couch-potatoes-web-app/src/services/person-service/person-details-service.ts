import {useQuery} from "react-query";
import {getConfig} from "../../configuration/configuration";
import {CacheKeys} from "../cache-keys";
import PersonDetails from "../../models/person/person-details";
import PersonMovieCredits from "../../models/person/person-movie-credits";
import PersonStats from "../../models/person/person-stats";

export function useFetchPersonDetailsAndCredits(options: FetchPersonDetailsOptions) {
    return useQuery({
        queryKey: [CacheKeys.PERSON_MOVIE_DETAILS_AND_CREDITS], queryFn: async () => {
            return {
                details: await fetchPersonDetails(options),
                credits: await fetchPersonMovieCredits(options),
                stats: await fetchPersonStats(options)
            }
        }
    })
}

export interface FetchPersonDetailsOptions {
    personId: number
}

// TODO: remember to fetch the base uri later from configs when we setup the gateway
async function fetchPersonDetails(options: FetchPersonDetailsOptions): Promise<PersonDetails> {
    const config = await getConfig();
    const {personId} = options;
    const response = await fetch(`${config.baseUrl}/person/details/${personId}`);
    if (!response.ok) throw new Error("Cannot fetch person details");
    return response.json();
}

async function fetchPersonMovieCredits(options: FetchPersonDetailsOptions): Promise<PersonMovieCredits> {
    const config = await getConfig();
    const {personId} = options;
    const response = await fetch(`${config.baseUrl}/person/movie-credits/${personId}`);
    if (!response.ok) throw new Error("Cannot fetch person movie credits");
    return response.json();
}

async function fetchPersonStats(options: FetchPersonDetailsOptions): Promise<PersonStats> {
    const config = await getConfig();
    const {personId} = options;
    const response = await fetch(`${config.baseUrl}/person/metrics/${personId}`);
    if (!response.ok) throw new Error("Cannot fetch person stats");
    return response.json();
}