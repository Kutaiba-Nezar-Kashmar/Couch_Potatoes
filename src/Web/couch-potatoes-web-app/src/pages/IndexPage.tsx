import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import BasePage from '../components/BasePage';
import {Avatar, Box, Flex, Input, InputGroup, InputLeftElement, Stack, Text, VStack} from "@chakra-ui/react";
import Movie from "../models/movie";
import {SearchIcon} from "@chakra-ui/icons";
import {navBarHeightInRem, pageHPaddingInRem} from "../components/settings/page-settings";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {useFetchPopularMovies} from "../Services/movie-collection";

const IndexPage = () => {
    const navigate = useNavigate();
    const TEST_PAGE_URL = '/test';
    const [movie, setMovie] = useState<Movie | null>(null);
    const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

    const { isLoading, isError, data, error } = useFetchPopularMovies(1, 3);
    function navigateToTestPage() {
        navigate(TEST_PAGE_URL);
    }

    if (isLoading)
        return <div>YOU SUCK ASS</div>;
    if (isError)
        console.log(error);
    return (
        <BackgroundImageFull imageUri={Background_Temp}>
            <BasePage>
                <Flex
                    align="center" justify="center"
                    height={`calc(100vh - ${navBarHeightInRem}rem)`}
                    width={`calc(100vw - ${2 * pageHPaddingInRem}rem)`}
                >
                    <VStack width="100%">
                        <Flex direction="row" justify="flex-end" width={{base: '300px', md: '450px', lg: '600px'}}>
                            <Text textColor="whites">
                                IMDB Score: {movie?.tmdbScore}
                            </Text>
                        </Flex>
                        <InputGroup width={{base: '300px', md: '450px', lg: '600px'}} backgroundColor="white">
                            <InputLeftElement
                                pointerEvents="none"
                                children={<SearchIcon color="gray.300"/>}
                            />
                            <Input type="text" placeholder="Search"/>
                        </InputGroup>
                    </VStack>
                </Flex>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default IndexPage;
