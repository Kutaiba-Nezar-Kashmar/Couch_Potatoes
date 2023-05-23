import {useQuery} from "react-query";
import {getConfig} from "../../configuration/configuration";
import {CacheKeys} from "../cache-keys";
import MultiSearchResponse from "../../models/search/multi-search-response";

export function useSearch(options: QueryOptions) {
    return useQuery({
        queryKey: [CacheKeys.SEARCH + "" + options.query], queryFn: async () => {
            return {
                multiSearch: await multiSearch(options)
            }
        }
    })
}

export interface QueryOptions {
    query?: string;
}

async function multiSearch(options: QueryOptions): Promise<MultiSearchResponse> {
    const config = await getConfig();
    const {query} = options;
    const response = await fetch(`${config.baseUrl}/search/multi/query=${query}`);
    if (!response.ok) throw new Error("Cannot perform multi search")
    return response.json();
}