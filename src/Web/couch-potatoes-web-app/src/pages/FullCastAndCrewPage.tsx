import React, {useEffect, useState} from "react";
import MovieCredits from "../models/movie_credits";
import {useNavigate, useParams} from "react-router-dom";
import {MovieCreditsAndDetails, useFetchMovieCreditsAndMovies} from "../services/movie-credits-and-details";
import {useFetchMovieCredits} from "../services/movie-credits";
import {
    Box, Button, Card, CardBody,
    Flex,
    Grid,
    GridItem, Heading, Image,
    Modal, ModalBody, ModalCloseButton,
    ModalContent,
    ModalOverlay,
    Spinner,
    Stack,
    StackDivider,
    Text
} from "@chakra-ui/react";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {getPosterImageUri} from "../services/images";
import BasePage from "../components/BasePage";
import {MovieDetailsHeaderInformationbox} from "../components/movie-details/MovieDetailsHeaderInformationbox";
import {Carousel} from "react-responsive-carousel";
import {MovieDetailsRightInformationbox} from "../components/movie-details/MovieDetailsRightInformationbox";
import {MovieDetailsBottomInformationbox} from "../components/movie-details/MovieDetailsBottomInformationbox";
import {MovieDetailsCastComponent} from "../components/movie-details/MovieDetailsCastComponent";
import {
    MovieDetailsRecommendedMoviesComponent
} from "../components/movie-details/MovieDetailsRecommendedMoviesComponent";
import {MovieDetailsReviewsComponent} from "../components/movie-details/MovieDetailsReviewsComponent";
import backgroundImageFull from "../components/BackgroundImageFull";
import {PersonCastCardComponent} from "../components/PersonCastCardComponent";
import {useFetchMovieDetails} from "../services/movie-details";
import Movie from "../models/movie";

const FullCastAndCrewPage = () => {
    const {movieId} = useParams()
    const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';
    const Empty_Profile_Pic = 'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png';

    const [movieCredits, setMovieCredits] = useState<MovieCredits | null>(null);
    const [movie, setMovie] = useState<Movie | null>(null);
    const themeColor = "teal";
    const navigate = useNavigate();
    const {isLoading, isError, data, error} = useFetchMovieCreditsAndMovies(Number(movieId));


    useEffect(() => {
        if (!isLoading) {
            setMovieCredits(
                data?.credits ?? null
            );
            setMovie(data?.movieDetails ?? null);
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
        <BackgroundImageFull imageUri={Background_Temp}>
            <BasePage>
                <Grid autoFlow={"dense"}
                      templateColumns={{base: "6fr", md: "repeat(6, 1fr)", lg: "repeat(6, 1fr)"}} gap={4}
                >
                    <GridItem justifySelf="center" alignSelf="center" colSpan={6}>
                        <Heading textTransform='uppercase' color={"white"}>Cast and crew for: {movie?.title} </Heading>
                    </GridItem>
                    <GridItem colSpan={6}>
                        <Button onClick={() => navigate(`/movie/details/${movie?.id}`)} margin={0.5}
                                colorScheme={themeColor} size='xs'
                        >Return to movie page</Button>
                    </GridItem>
                    <GridItem colSpan={3} gap={4}>
                        <Stack direction={"row"}>
                            <Heading color={"white"}>Cast </Heading>
                            <Heading color={"grey"}>({movieCredits?.creditsAsCast.length}) </Heading>
                        </Stack>
                        <Box borderRadius={"lg"} bg={"white"}>
                            {movieCredits?.creditsAsCast.map((cast) => (
                                <Box marginBottom={4}>
                                    <Stack padding={2} divider={<StackDivider borderColor='gray.300'/>}
                                           onClick={() => navigate(`/person/${cast.id}`)} _hover={{cursor: "pointer"}}
                                           direction={"row"} gap={2}>
                                        {(cast?.profilePath) ? (<Image
                                            height={"130px"}
                                            src={getPosterImageUri(cast?.profilePath)}
                                            alt='Profile picture of actor'
                                            borderRadius='lg'
                                        />) : (<Image
                                            height={"130px"}
                                            src={Empty_Profile_Pic}
                                            alt='No image available of actor'
                                            borderRadius='lg'
                                        />)}
                                        <Stack direction={"column"}>
                                            <Text fontSize={"lg"}>{cast.name ? cast.name : "N/A"}</Text>
                                            <Text fontStyle="italic" color={themeColor}
                                                  fontSize='md'>{cast.character ? cast.character : "N/A"}</Text>
                                        </Stack>
                                    </Stack>
                                </Box>
                            ))}
                        </Box>

                    </GridItem>


                    <GridItem colSpan={3} gap={4}>
                        <Stack direction={"row"}>
                            <Heading color={"white"}>Crew </Heading>
                            <Heading color={"grey"}>({movieCredits?.creditsAsCrew.length}) </Heading>
                        </Stack>

                        <Box borderRadius={"lg"} bg={"white"}>
                            {movieCredits?.creditsAsCrew.map((crew) => (
                                <Box marginBottom={4}>
                                    <Stack padding={2} divider={<StackDivider borderColor='gray.300'/>}
                                           _hover={{cursor: "pointer"}} onClick={() => navigate(`/person/${crew.id}`)}
                                           direction={"row"} gap={2}>
                                        {(crew?.profilePath) ? (<Image
                                            height={"130px"}
                                            src={getPosterImageUri(crew?.profilePath)}
                                            alt='Profile picture of actor'
                                            borderRadius='lg'

                                        />) : (<Image
                                            height={"130px"}

                                            src={Empty_Profile_Pic}
                                            alt='No image available of actor'
                                            borderRadius='lg'
                                        />)}
                                        <Stack direction={"column"}>
                                            <Text fontSize={"lg"}>{crew.name ? crew.name : "N/A"}</Text>
                                            <Text fontStyle="italic" color={themeColor}
                                                  fontSize='md'>{crew.job ? crew.job : "N/A"}</Text>
                                        </Stack>
                                    </Stack>
                                </Box>
                            ))}
                        </Box>

                    </GridItem>

                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};
export default FullCastAndCrewPage