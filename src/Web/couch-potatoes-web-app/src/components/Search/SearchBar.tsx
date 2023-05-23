import React, {FC, useEffect, useState} from "react";
import {Box, Input, InputGroup, InputLeftElement, List, ListItem, VStack, Text} from "@chakra-ui/react";
import {SearchIcon} from "@chakra-ui/icons";
import {useFetchPersonDetailsAndCredits} from "../../services/person-service/person-details-service";
import {multiSearch} from "../../services/search/search-service";
import MultiSearchResponse from "../../models/search/multi-search-response";

export const SearchBar = () => {
    function debounce(func: Function, delay: number) {
        let timerId: NodeJS.Timeout;

        return (...args: any[]) => {
            clearTimeout(timerId);

            timerId = setTimeout(() => {
                func.apply(null, args);
            }, delay);
        };
    }

    const [searchTerm, setSearchTerm] = useState("");
    const [searchResults, setSearchResults] = useState<MultiSearchResponse | null>(null);
    const handleSearchInputChange = (event: any) => {
        const {value} = event.target;
        setSearchTerm(value);
    };
    const debouncedSearchTerm = debounce(handleSearchInputChange, 3000);
    const performSearch = async () => {
    const result = await multiSearch(searchTerm);
        setSearchResults(result ?? null);
    };

    // Execute the search logic when the debounced search term changes
    useEffect(() => {
        performSearch();
    }, [searchTerm]);

    return (
        <>
            <VStack spacing={4} align="stretch">
                <InputGroup width={{base: '300px', md: '450px', lg: '600px'}}>
                    <InputLeftElement
                        pointerEvents="none"
                        children={<SearchIcon color="gray.300"/>}
                    />
                    <Input value={searchTerm} onChange ={handleSearchInputChange} bg="white" type="text"
                           placeholder="Search"/>
                </InputGroup>
                <Box>
                    <List spacing={2}>
                        {searchResults?.movies?.map((result) => (
                            <ListItem key={result.id}>
                                <Text color='white'>{result.title}</Text>
                            </ListItem>
                        ))}
                    </List>
                </Box>
            </VStack>
        </>
    )
}