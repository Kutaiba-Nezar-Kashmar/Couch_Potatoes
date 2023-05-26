import React, {FC, useEffect, useState} from "react";
import {
    Box,
    Input,
    InputGroup,
    InputLeftElement,
    List,
    ListItem,
    VStack,
    Text,
    Stack,
    Image,
    Flex
} from "@chakra-ui/react";
import {SearchIcon} from "@chakra-ui/icons";
import {multiSearch} from "../../services/search/search-service";
import MultiSearchResponse from "../../models/search/multi-search-response";
import {getPosterImageUri} from "../../services/images";
import {useNavigate} from "react-router-dom";

const scrollbarStyles = {
    width: "8px",
    backgroundColor: "white",
    borderRadius: "4px",
};

const hoverStyles = {
    backgroundColor: "darkgray",
};

interface searchBarProperties {
    width: number
}

export const SearchBar: FC<searchBarProperties> = ({width}) => {
    const Background_Temp =
        'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png';

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

    const debouncedSearchTerm = debounce(handleSearchInputChange, 1000);
    const performSearch = async () => {
        const result = await multiSearch(searchTerm);
        setSearchResults(result ?? null);
    };
    const navigate = useNavigate();

    // Execute the search logic when the debounced search term changes
    useEffect(() => {
        performSearch();
    }, [searchTerm]);

    return (
        <>
            <VStack spacing={4} align="stretch">
                <InputGroup width={[width / 2.5, width]}>
                    <InputLeftElement
                        pointerEvents="none"
                        children={<SearchIcon color="gray.300"/>}
                    />
                    <Input onChange={debouncedSearchTerm} bg="white" type="text"
                           placeholder="Search"/>
                </InputGroup>
                <Box>
                    {searchTerm && (
                        <List spacing={"1px"} boxShadow="md" position={"absolute"} bg={"grey"}
                              width={[width / 2, width]} maxHeight="300px"
                              overflow="auto" zIndex="9999" sx={{
                            "&::-webkit-scrollbar": scrollbarStyles,
                            "&::-webkit-scrollbar-thumb": hoverStyles,
                            "&::-webkit-scrollbar-thumb:hover": hoverStyles,
                        }}>
                            {searchResults?.movies?.map((result) => (
                                <ListItem padding={"2px"} bg={"white"} key={result.id} onClick={() =>
                                    navigate(`/Couch_Potatoes/movie/details/${result.id}`)}>
                                    <Stack
                                        direction={"row"} overflowX={"auto"} _hover={{cursor: "pointer"}}>
                                        {(result?.posterPath) ? (<Image maxHeight={"90px"}
                                                                        src={getPosterImageUri(result?.posterPath)}
                                                                        alt={"poster of movie" + result.title}
                                                                        borderRadius='lg'
                                        />) : (<Image maxHeight={"80px"}
                                                      maxWidth={"60px"}
                                                      src={Background_Temp}
                                                      alt={"no poster of movie" + result.title}
                                                      borderRadius='lg'
                                        />)}
                                        <Flex alignItems={"center"}>
                                            <Stack gap={0} direction={"column"}>
                                                <Text>{result.title}</Text>
                                                <Text fontStyle={"italic"}
                                                      color={"grey"}>{result.releaseDate?.slice(0, 4)}</Text>
                                            </Stack>

                                        </Flex>

                                    </Stack>

                                </ListItem>

                            ))}
                            {searchResults?.people?.map((result) => (
                                <ListItem padding={"2px"} bg={"white"} key={result.id} onClick={() =>
                                    navigate(
                                        `/Couch_Potatoes/person/${result.id}`
                                    )
                                }>
                                    <Stack
                                        direction={"row"} overflowX={"auto"} _hover={{cursor: "pointer"}}>
                                        {(result?.profilePath) ? (<Image maxHeight={"90px"}
                                                                         src={getPosterImageUri(result?.profilePath)}
                                                                         alt={"poster of person" + result.name}
                                                                         borderRadius='lg'
                                        />) : (<Image maxHeight={"90px"}
                                                      maxWidth={"60px"}
                                                      src={Background_Temp}
                                                      alt={"no poster of person" + result.name}
                                                      borderRadius='lg'
                                        />)}
                                        <Flex alignItems={"center"}>
                                            <Stack direction={"column"}>
                                                <Text>{result.name}</Text>
                                                <Text color={"grey"} fontSize={"sm"} fontStyle={"italic"}>Known
                                                    for: {result.knownFor}</Text>
                                            </Stack>

                                        </Flex>

                                    </Stack>

                                </ListItem>

                            ))}
                        </List>)}

                </Box>
            </VStack>
        </>
    )
}