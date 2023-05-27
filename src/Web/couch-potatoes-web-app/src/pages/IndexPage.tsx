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
    Button, Stack, Grid, GridItem, Heading, calc
} from "@chakra-ui/react";
import StarRatingComponent from 'react-star-rating-component';
import Movie from "../models/movie";
import {SearchIcon} from "@chakra-ui/icons";
import {navBarHeightInRem, pageHPaddingInRem} from "../components/settings/page-settings";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {useFetchAllMovieCollections} from "../services/movie-collection";
import {getPosterImageUri} from "../services/images";
import FrontPageMovieInfoBoxComponent from "../components/FrontPageMovieInfoBoxComponent";
import MovieRecommendations from "../models/movie-Recommedations";
import {FrontPageCollectionViewComponent} from "../components/FrontPageCollectionViewComponent";
import moviePoster from '../assets/MoviePosterTemp.png';


import {Swiper, SwiperSlide} from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/navigation";
import {sliceNumber} from "../util/numberutil";
import {SearchBar} from "../components/Search/SearchBar";


const IndexPage = () => {
    const navigate = useNavigate();
    const TEST_PAGE_URL = '/test';
    const [popularMovies, setPopularMovies] = useState<MovieRecommendations | null>(null);
    const [topRatedMovies, setTopRatedMovies] = useState<MovieRecommendations | null>(null);
    const [NowPlayingMovies, setNowPlayingMovies] = useState<MovieRecommendations | null>(null);
    const [featuredMovie, setFeaturedMovie] = useState<Movie | null>(null);
    const Background_Temp =moviePoster;

    const {isLoading, isError, data, error} = useFetchAllMovieCollections();


    useEffect(() => {
        if (!isLoading) {
            setPopularMovies(
                data?.popularMovies ?? null
            );
            setNowPlayingMovies(
                data?.NowPlayingMovies ?? null
            );
            setTopRatedMovies(
                data?.topRatedMovies ?? null);
            setFeaturedMovie(
                popularMovies?.collection[0] ?? null);

        }
    }, [isLoading, popularMovies])

    if (isLoading && !featuredMovie) {
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
        <BackgroundImageFull
            imageUri={featuredMovie?.imageUri ? getPosterImageUri(featuredMovie?.imageUri) : Background_Temp}>
            <BasePage>

                    <Grid templateRows="2fr" templateColumns={{base: "6fr", md: "repeat(6, 1fr)", lg: "repeat(6, 1fr)"}}

                          gap={4}>
                        <GridItem colSpan={6} rowSpan={17}>
                        {/*TODO: Just a centering hack*/}
                        </GridItem>
                        <GridItem alignItems="center" colSpan={6}>
                            <Flex align="center" justify="center">
                                {/* TMDB SCORE */}
                                <Stack direction="column">
                                    <Flex direction="row" justify="flex-end"
                                          width={{base: '300px', md: '450px', lg: '600px'}}>
                                        <Text textColor="white">
                                            IMDB Score: {sliceNumber(Number(popularMovies?.collection[0]?.tmdbScore),1) } / 10
                                        </Text>
                                    </Flex>
                                    {/* SEARCH */}
                                    <SearchBar width={600}/>
                                </Stack>

                            </Flex>

                        </GridItem>

                        <GridItem colSpan={6}>
                            {/* PLAY TRAILER */}
                            <Flex direction="row" justify="flex-start" width="100%">
                                {popularMovies?.collection[0]?.trailerUri && <Button size="md" colorScheme="red"
                                                                                     marginTop="0.5rem">{/*NOTE: (mibui 2023-05-15) Only display this button if there is a trailer uri */}
                                    <a href={popularMovies?.collection[0]?.trailerUri} target="_blank">
                                        Play Trailer
                                    </a>
                                </Button>}
                            </Flex>
                        </GridItem>

                        <GridItem colSpan={6}>
                            {/* FEATURED MOVIE INFO */}
                            <FrontPageMovieInfoBoxComponent movie={featuredMovie!}/>
                        </GridItem>
                        <GridItem colSpan={6}>
                            <Heading color="white">Popular</Heading>
                            <FrontPageCollectionViewComponent movieCollection={popularMovies!}/>

                        </GridItem>

                        <GridItem colSpan={6}>
                            <Heading color="white">Now Playing</Heading>
                            <FrontPageCollectionViewComponent movieCollection={NowPlayingMovies!}/>
                        </GridItem>
                        <GridItem colSpan={6}>
                            <Heading color="white">Top Rated</Heading>
                            <FrontPageCollectionViewComponent movieCollection={topRatedMovies!}/>
                        </GridItem>
                    </Grid>


            </BasePage>
        </BackgroundImageFull>
    );
};

export default IndexPage;
