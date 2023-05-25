import {useQuery} from "react-query";
import {getConfig} from "../../configuration/configuration";
import {CacheKeys} from "../cache-keys";
import MultiSearchResponse from "../../models/search/multi-search-response";

export async function multiSearch(query: string): Promise<MultiSearchResponse> {
    const config = await getConfig();
    if (!query) return {movies: [], people: []}
    const response = await fetch(`${config.baseUrl}/search/multi?query=${query}`);
    if (!response.ok) throw new Error("Cannot perform multi search")
    return response.json();
}