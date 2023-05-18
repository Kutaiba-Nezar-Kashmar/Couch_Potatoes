import {useQuery} from "react-query";
import {CacheKeys} from "../cache-keys";
import {getConfig} from "../../configuration/configuration";
import PersonDetails from "../../models/person/person-details";

export function useFetchPersonDetails(options: FetchPersonDetailsOptions) {
    return useQuery({
        queryKey: [CacheKeys.PERSON_DETAILS + options.personId], queryFn: async (): Promise<object> => {
            return {
                person: await fetchPersonDetails(options)
            }
        }
    })
}

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