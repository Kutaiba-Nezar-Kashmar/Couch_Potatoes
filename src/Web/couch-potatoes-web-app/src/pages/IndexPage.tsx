import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import BasePage from '../components/BasePage';
import {
    Avatar,
    Box,
    Flex,
    Input,
    InputGroup,
    InputLeftElement,
    Spinner,
    Text,
    VStack,
    HStack,
    Button, Stack
} from "@chakra-ui/react";
import StarRatingComponent from 'react-star-rating-component';
import Movie from "../models/movie";
import {SearchIcon} from "@chakra-ui/icons";
import {navBarHeightInRem, pageHPaddingInRem} from "../components/settings/page-settings";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {useFetchAllMovieCollections, useFetchCollections, useFetchPopularMovies} from "../services/movie-collection";
import {getPosterImageUri} from "../services/images";
import FrontPageMovieInfoBoxComponent from "../components/FrontPageMovieInfoBoxComponent";
import MovieRecommendations from "../models/movie-Recommedations";
import {FrontPageCollectionViewComponent} from "../components/FrontPageCollectionViewComponent";

const IndexPage = () => {
    const navigate = useNavigate();
    const TEST_PAGE_URL = '/test';
    const [popularMovies, setPopularMovies] = useState<MovieRecommendations | null>(null);
    const [topRatedMovies, setTopRatedMovies] = useState<MovieRecommendations | null>(null);
    const [nowPlayingMovies, setNowPlayingMovies] = useState<MovieRecommendations | null>(null);
    const [featuredMovie, setFeaturedMovie] = useState<Movie | null>(null);
    const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

    const {isLoading, isError, data, error} = useFetchAllMovieCollections();

    // NOTE: (mibui 2023-05-15) Notice we use useEffect here to subscribe to the loading state of the useQuery hook.
    //                          In the dependency array we put isLoading to ensure that the page rerenders everytime
    //                          we are fetching data. Under the hood we are subscribing to some event emitter that
    //                          react-query provides.
    useEffect(() => {
        if (!isLoading) {
            // NOTE: (mibui 2023-05-15) Take the most popular movie as featured.
            // TODO: (mibui 2023-05-15) Fetch from /movies/:movieid instead of taking from collection.
            //                          e.g. setMovie(getMovie((collections as any)!["popular"]["collection"][0]?.id))
            setPopularMovies(
                data?.popularMovies?? null
            );
            setNowPlayingMovies(
                data?.NowPlayingMovies ?? null
            );
            setNowPlayingMovies(
                data?.topRatedMovies ?? null); // TODO: Remove the genres, they are only there for testing purposes
            setFeaturedMovie(
                popularMovies?.collection[0] ??null);

        }
    }, [isLoading])

    if (isLoading) {
        return <Flex width="100%" height="100%" justifyContent="center" alignItems="center">
            <Spinner
                thickness='4px'
                speed='0.65s'
                emptyColor='gray.200'
                color='blue.500'
                size='xl'
            />
        </Flex>
    }

    if (isError) {
        console.log(error);
    }

    if (data) {
        console.log(data);
    }

    return (
        <BackgroundImageFull imageUri={getPosterImageUri(popularMovies?.collection[0].imageUri as string) || Background_Temp}>
            <BasePage>

                <Flex
                    align="center" justify="center"
                    height={`calc(100vh - ${navBarHeightInRem}rem)`}
                    width={`calc(100vw - ${2 * pageHPaddingInRem}rem)`}
                >
                    <VStack width={{base: '300px', md: '450px', lg: '600px'}}>
                        {/* TMDB SCORE */}
                        <Flex direction="row" justify="flex-end" width={{base: '300px', md: '450px', lg: '600px'}}>
                            <Text textColor="white">
                                IMDB Score: {popularMovies?.collection[0]?.tmdbScore} / 10
                            </Text>
                        </Flex>
                        {/* SEARCH */}
                        <InputGroup width={{base: '300px', md: '450px', lg: '600px'}} >
                            <InputLeftElement
                                pointerEvents="none"
                                children={<SearchIcon color="gray.300"/>}
                            />
                            <Input bg="white" type="text" placeholder="Search"/>
                        </InputGroup>
                        {/* PLAY TRAILER */}
                        <Flex direction="row" justify="flex-start" width="100%">
                            {popularMovies?.collection[0]?.trailerUri && <Button size="md" colorScheme="red"
                                                          marginTop="0.5rem">{/*NOTE: (mibui 2023-05-15) Only display this button if there is a trailer uri */}
                                <a href={popularMovies?.collection[0]?.trailerUri} target="_blank">
                                    Play Trailer
                                </a>
                            </Button>}
                        </Flex>
                        {/* FEATURED MOVIE INFO */}
                       <FrontPageMovieInfoBoxComponent movie={featuredMovie}/>
                        <Stack direction="column">
                            <FrontPageCollectionViewComponent movieCollection={popularMovies!}></FrontPageCollectionViewComponent>
                            <FrontPageCollectionViewComponent movieCollection={topRatedMovies!}></FrontPageCollectionViewComponent>
                            <FrontPageCollectionViewComponent movieCollection={nowPlayingMovies!}></FrontPageCollectionViewComponent>

                        </Stack>

                    </VStack>
                </Flex>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default IndexPage;
