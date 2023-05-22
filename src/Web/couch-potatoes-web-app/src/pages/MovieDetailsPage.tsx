import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
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
    Button,
    Stack,
    Grid,
    GridItem,
    StackDivider,
    useBreakpointValue,
    Modal,
    ModalOverlay,
    ModalContent,
    ModalCloseButton,
    ModalBody,
    Image,
    Card,
    CardHeader,
    Heading,
    CardBody,
    Link,
    Divider,
    background,
    color,
    useToast,
} from '@chakra-ui/react';
import Movie from '../models/movie';
import BackgroundImageFull from '../components/BackgroundImageFull';
import { getPosterImageUri } from '../services/images';
import 'react-responsive-carousel/lib/styles/carousel.min.css'; // requires a loader
import { Carousel } from 'react-responsive-carousel';
import { useFetchMovieDetails } from '../services/movie-details';
import { Swiper, SwiperSlide } from 'swiper/react';
import { Autoplay, Navigation, Pagination, Scrollbar } from 'swiper';
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

import {
    MovieCreditsAndDetails,
    useFetchMovieCreditsAndMovies,
} from '../services/movie-credits-and-details';
import MovieCredits from '../models/movie_credits';
import colors from 'tailwindcss/colors';
import { MovieDetailsHeaderInformationbox } from '../components/movie-details/MovieDetailsHeaderInformationbox';
import { MovieDetailsRightInformationbox } from '../components/movie-details/MovieDetailsRightInformationbox';
import { MovieDetailsBottomInformationbox } from '../components/movie-details/MovieDetailsBottomInformationbox';
import { MovieDetailsCastComponent } from '../components/movie-details/MovieDetailsCastComponent';
import { MovieDetailsReviewsComponent } from '../components/movie-details/MovieDetailsReviewsComponent';
import MovieRecommendations from '../models/movie-Recommedations';
import { MovieDetailsRecommendedMoviesComponent } from '../components/movie-details/MovieDetailsRecommendedMoviesComponent';
import {
    UserCacheKeys,
    addFavoriteMovie,
    getAuthenticatedUser,
} from '../services/user';
import { useQueryClient } from 'react-query';
import User from '../models/user';

const MovieDetailsPage = () => {
    const { movieId } = useParams();
    const [isOpen, setIsOpen] = useState(false);
    const [expandedImage, setExpandedImage] = useState('');
    const Background_Temp =
        'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png';
    const handleImageClick = (imageUrl: string) => {
        setExpandedImage(imageUrl);
        setIsOpen(true);
    };

    const handleModalClose = () => {
        setExpandedImage('');
        setIsOpen(false);
    };

    const themeColor = 'teal';
    const emptyCreditsObject: MovieCredits = {
        id: 1,
        creditsAsCast: [],
        creditsAsCrew: [],
    };

    const navigate = useNavigate();

    const [movie, setMovie] = useState<Movie | null>(null);
    const [recommendedMovies, setRecommendedMovies] =
        useState<MovieRecommendations | null>(null);
    const [movieCredits, setMovieCredits] = useState<MovieCredits | null>(null);
    const [authenticatedUser, setAuthenticatedUser] = useState<User | null>(
        null
    );

    const { isLoading, isError, data, error } = useFetchMovieCreditsAndMovies(
        Number(movieId)
    );

    async function initUser() {
        const user = await getAuthenticatedUser();
        setAuthenticatedUser(user);
    }

    function convertToHoursAndMinutes(num: number) {
        const hours = Math.floor(num / 60); // Get the number of hours
        const minutes = num % 60; // Get the remaining minutes

        return `${hours}h ${minutes}m`; // Return the formatted string
    }

    useEffect(() => {
        initUser();
        if (!isLoading) {
            const detailsAndCredits = data as MovieCreditsAndDetails;
            setMovie(data?.movieDetails ?? null);
            setMovieCredits(data?.credits ?? null);
            setRecommendedMovies(data?.movieRecommendations ?? null);
        }
    }, [isLoading, authenticatedUser]);

    if (isLoading) {
        return (
            <Flex
                width="100%"
                height="100%"
                justifyContent="center"
                alignItems="center"
            >
                <Spinner
                    thickness="4px"
                    speed="0.65s"
                    emptyColor="gray.200"
                    color="blue.500"
                    size="xl"
                />
            </Flex>
        );
    }

    if (isError) {
        console.log(error);
    }

    if (data) {
        console.log(data);
    }

    return (
        <BackgroundImageFull
            imageUri={getPosterImageUri(movie?.backdropUri as string)}
        >
            <BasePage>
                <Grid
                    templateColumns={{
                        base: '6fr',
                        md: 'repeat(6, 1fr)',
                        lg: 'repeat(6, 1fr)',
                    }}
                    gap={4}
                >
                    {/* Movie header information */}
                    <GridItem colSpan={6} rowSpan={1}>
                        <MovieDetailsHeaderInformationbox
                            movie={movie}
                            themeColor={themeColor}
                            user={authenticatedUser}
                        />
                    </GridItem>

                    {/*  YEAR AND RUNTIME */}
                    <GridItem colSpan={6} rowSpan={1}>
                        <Stack
                            direction="row"
                            divider={<StackDivider borderColor="gray.200" />}
                        >
                            <Box>
                                <Text color={'white'} fontSize="lg">
                                    {convertToHoursAndMinutes(
                                        movie?.runTime as number
                                    )}
                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>
                    {/* CAROUSEL */}
                    <GridItem colSpan={5} rowSpan={4}>
                        <Box>
                            <Carousel
                                infiniteLoop={true}
                                autoPlay={true}
                                interval={3000}
                                stopOnHover={true}
                            >
                                <Box
                                    _hover={{ cursor: 'pointer' }}
                                    onClick={() =>
                                        handleImageClick(
                                            getPosterImageUri(
                                                movie?.imageUri as string
                                            )
                                        )
                                    }
                                >
                                    <img
                                        style={{
                                            width: '100%',
                                            maxHeight: '500px',
                                            height: 'auto',
                                            objectFit: 'contain',
                                        }}
                                        src={getPosterImageUri(
                                            movie?.imageUri as string
                                        )}
                                    />
                                </Box>
                                <Box
                                    _hover={{ cursor: 'pointer' }}
                                    onClick={() =>
                                        handleImageClick(
                                            getPosterImageUri(
                                                movie?.backdropUri as string
                                            )
                                        )
                                    }
                                >
                                    <img
                                        style={{
                                            width: '100%',
                                            maxHeight: '500px',
                                            height: 'auto',
                                            objectFit: 'contain',
                                        }}
                                        src={getPosterImageUri(
                                            movie?.backdropUri as string
                                        )}
                                    />
                                </Box>
                            </Carousel>
                            <Modal
                                isOpen={isOpen}
                                onClose={handleModalClose}
                                size="6xl"
                            >
                                <ModalOverlay />
                                <ModalContent>
                                    <ModalCloseButton />
                                    <ModalBody justifyContent="center">
                                        <Image
                                            src={expandedImage}
                                            alt="Expanded Image"
                                            mx="auto"
                                            maxHeight={750}
                                        />
                                    </ModalBody>
                                </ModalContent>
                            </Modal>
                        </Box>
                    </GridItem>

                    {/* INFORMATIONBAR RIGHT */}
                    <GridItem rowSpan={6} colSpan={1}>
                        <MovieDetailsRightInformationbox
                            movie={movie}
                            themeColor={themeColor}
                        />
                    </GridItem>

                    {/* INFORMATIONBAR BOTTOM */}

                    <GridItem colSpan={5} rowSpan={1}>
                        <MovieDetailsBottomInformationbox
                            movie={movie}
                            movieCredits={movieCredits}
                            themeColor={themeColor}
                        />
                    </GridItem>

                    {/*CAST*/}
                    <GridItem colSpan={5}>
                        <MovieDetailsCastComponent
                            themeColor={themeColor}
                            Background_Temp={Background_Temp}
                            movieCredits={movieCredits ?? emptyCreditsObject}
                        />
                    </GridItem>
                    {/*Recommended Movies*/}
                    <GridItem colSpan={5}>
                        <MovieDetailsRecommendedMoviesComponent
                            themeColor={themeColor}
                            Background_Temp={Background_Temp}
                            movieRecommendations={recommendedMovies}
                        />
                    </GridItem>

                    {/*REVIEWS*/}
                    <GridItem colSpan={5} rowSpan={1}>
                        <MovieDetailsReviewsComponent />
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default MovieDetailsPage;
