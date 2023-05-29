import React, {useState, useEffect} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import BasePage from '../components/BasePage';
import {
    Box,
    Flex,
    Spinner,
    Text,
    Stack,
    Grid,
    GridItem,
    StackDivider,
    Modal,
    ModalOverlay,
    ModalContent,
    ModalCloseButton,
    ModalBody,
    Image, Tabs, TabList, Tab, TabPanels, TabPanel, Heading, AspectRatio,
} from '@chakra-ui/react';
import Movie from '../models/movie';
import BackgroundImageFull from '../components/BackgroundImageFull';
import {getPosterImageUri} from '../services/images';
import 'react-responsive-carousel/lib/styles/carousel.min.css'; // requires a loader
import {Carousel} from 'react-responsive-carousel';

import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

import {
    MovieCreditsAndDetails,
    useFetchMovieCreditsAndMovies,
} from '../services/movie-credits-and-details';
import MovieCredits from '../models/movie_credits';

import {MovieDetailsHeaderInformationbox} from '../components/movie-details/MovieDetailsHeaderInformationbox';
import {MovieDetailsRightInformationBox} from '../components/movie-details/MovieDetailsRightInformationBox';
import {MovieDetailsBottomInformationBox} from '../components/movie-details/MovieDetailsBottomInformationBox';
import {MovieDetailsCastComponent} from '../components/movie-details/MovieDetailsCastComponent';
import {MovieDetailsReviewsComponent} from '../components/movie-details/MovieDetailsReviewsComponent';
import MovieRecommendations from '../models/movie-Recommedations';
import {
    UserCacheKeys,
    addFavoriteMovie,
    getAuthenticatedUser,
    useGetAuthenticatedUser,
} from '../services/user';
import {useQueryClient} from 'react-query';
import User from '../models/user';
import {
    MovieDetailsRecommendedMoviesComponent
} from '../components/movie-details/MovieDetailsRecommendedMoviesComponent';
import {useGetReviewsForMovie} from '../services/review';
import {Review} from '../models/review/review';
import {Swiper, SwiperSlide} from "swiper/react";
import {Navigation} from "swiper";
import PersonMovieCreditsItem from "../components/person/PersonMovieCreditsItem";
import Video from "../models/Video";
import moviePoster from '../assets/MoviePosterTemp.png';

const MovieDetailsPage = () => {
    const {movieId} = useParams();
    const [isOpen, setIsOpen] = useState(false);
    const [expandedImage, setExpandedImage] = useState('');
    const Empty_profilePic =
        'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png';
    const Background_Temp=moviePoster;

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
    const [reviews, setReviews] = useState<Review[]>([]);

    const {isLoading, isError, data, error} = useFetchMovieCreditsAndMovies(
        Number(movieId)
    );

    const {
        isLoading: isLoadingReviews,
        isError: isErrorReview,
        data: reviewData,
        error: reviewError,
    } = useGetReviewsForMovie(Number(movieId));

    const {
        isLoading: isLoadingUser,
        isError: isErrorUser,
        data: userData,
        error: userError,
    } = useGetAuthenticatedUser();

    function convertToHoursAndMinutes(num: number) {
        const hours = Math.floor(num / 60); // Get the number of hours
        const minutes = num % 60; // Get the remaining minutes

        return `${hours}h ${minutes}m`; // Return the formatted string
    }

    function sortTrailers() {
        if (!movie) {
            return []
        }

        const trailers: Video[] = [];
        const otherVideos: Video[] = [];
        movie!.videos.forEach(v => {
            if (v.type === "Trailer") {
                trailers.push(v)
            } else
                otherVideos.push(v)

        })
        return trailers.concat(otherVideos)
    }

    useEffect(() => {
        if (!isLoadingUser) {
            setAuthenticatedUser(userData ?? null);
        }
        if (!isLoading) {
            const detailsAndCredits = data as MovieCreditsAndDetails;
            setMovie(data?.movieDetails ?? null);
            setMovieCredits(data?.credits ?? null);
            setRecommendedMovies(data?.movieRecommendations ?? null);
        }

        if (!isLoadingReviews) {
            setReviews(reviewData ?? []);
        }
    }, [isLoading, isLoadingUser, isLoadingReviews]);

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
            imageUri={(movie?.backdropUri ? (getPosterImageUri(movie?.backdropUri as string)) : (Background_Temp))}
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
                            divider={<StackDivider borderColor="gray.200"/>}
                        >
                            <Box>
                                <Text color={'white'} fontSize="lg">
                                    {movie?.runTime ? (convertToHoursAndMinutes(
                                        movie?.runTime as number
                                    )) : ("N/A")}

                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>
                    {/* CAROUSEL */}
                    <GridItem colSpan={5} rowSpan={4}>
                        <Box>

                            {movie &&
                                <Carousel
                                    infiniteLoop={true}
                                  showStatus={false}
                                    showIndicators={false}
                                    swipeable={true}
                                >
                                    {/*{movie?.videos.filter(v =>v.type==="Trailer").map((video) =>(*/}
                                    {/*    // <Box> hey</Box>*/}
                                    {/*// ))}*/}
                                    {/*TODO: FIIIx*/}


                                    {
                                        sortTrailers().slice(0, 1).map((video) => (
                                            <Box >
                                                <Box mx="auto">
                                                    <AspectRatio width={"100%"} maxHeight={"500px"} ratio={16 / 9}>
                                                        <iframe
                                                                src={"https://www.youtube.com/embed/" + video.key}
                                                                title={video.name}
                                                                allowFullScreen
                                                        />
                                                    </AspectRatio>
                                                </Box>
                                            </Box>
                                        )).concat(movie?.backdrops?.filter(b => b.lang === "en").slice(0, 10).map((backDrop) => (
                                            <Box
                                                _hover={{cursor: 'pointer'}}
                                                onClick={() =>
                                                    handleImageClick(backDrop.filePath ? (getPosterImageUri(backDrop?.filePath!)) : (Background_Temp)
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
                                                    src={(movie?.backdropUri ? (getPosterImageUri(backDrop?.filePath!)) : (Background_Temp))}
                                                />
                                            </Box>)) as any[])

                                    }


                                </Carousel>
                            }
                            <Modal
                                isOpen={isOpen}
                                onClose={handleModalClose}
                                size="6xl"
                            >
                                <ModalOverlay/>
                                <ModalContent>
                                    <ModalCloseButton/>
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
                        <MovieDetailsRightInformationBox
                            movie={movie}
                            themeColor={themeColor}
                        />
                    </GridItem>

                    {/* INFORMATIONBAR BOTTOM */}

                    <GridItem colSpan={5} rowSpan={1}>
                        <MovieDetailsBottomInformationBox
                            movie={movie}
                            movieCredits={movieCredits ?? emptyCreditsObject}
                            themeColor={themeColor}
                        />
                    </GridItem>

                    {/*CAST*/}
                    <GridItem colSpan={5}>
                        <MovieDetailsCastComponent
                            themeColor={themeColor}
                            Background_Temp={Empty_profilePic}
                            movieCredits={movieCredits ?? emptyCreditsObject}
                        />
                    </GridItem>
                    {/*Recommended Movies*/}
                    <GridItem colSpan={5}>
                        <MovieDetailsRecommendedMoviesComponent
                            themeColor={themeColor}
                            Background_Temp={Empty_profilePic}
                            movieRecommendations={recommendedMovies}
                        />
                    </GridItem>
                    <GridItem colSpan={5}>
                        <Tabs padding={"25px"} borderRadius={"lg"} bg={"white"}>
                            <Stack direction={"row"}> <Flex alignItems={"center"}> <Heading size={"md"}>Media</Heading></Flex>
                                <TabList>
                                    <Tab><Text marginX={2}>Videos</Text> <Text
                                        color={"grey"}> {movie?.videos.length}</Text></Tab>
                                    <Tab><Text marginX={2}>Posters</Text> <Text
                                        color={"grey"}> {movie?.posters.filter(p => p.lang === "en").length}</Text></Tab>
                                    <Tab><Text marginX={2}>Backdrops</Text> <Text
                                        color={"grey"}> {movie?.backdrops.filter(b => b.lang === "en").length}</Text></Tab>
                                </TabList></Stack>
                            <TabPanels>
                                <TabPanel>


                                    <Swiper
                                        navigation={true}
                                        modules={[Navigation]}
                                        className="mySwiper"
                                        slidesPerView={3}
                                        pagination={{clickable: true}}
                                        spaceBetween={5}
                                    >
                                        {sortTrailers().map((video) => (
                                            <SwiperSlide>
                                                <Box mx="auto">
                                                    <AspectRatio ratio={4 / 3}>
                                                        <iframe
                                                            src={"https://www.youtube.com/embed/" + video.key}
                                                            title={video.name}
                                                            allowFullScreen
                                                        />
                                                    </AspectRatio>
                                                </Box>
                                                <Text color={"grey"}>{video.type}</Text>
                                            </SwiperSlide>
                                        ))}
                                    </Swiper>


                                </TabPanel>
                                <TabPanel>
                                    <Swiper
                                        navigation={true}
                                        modules={[Navigation]}
                                        className="mySwiper"
                                        slidesPerView={5}
                                        pagination={{clickable: true}}
                                        spaceBetween={5}
                                    >
                                        {movie?.posters.filter(p => p.lang === "en").map((poster) => (
                                            <SwiperSlide
                                                onClick={() =>
                                                    handleImageClick(
                                                        getPosterImageUri(
                                                            poster?.filePath
                                                        )
                                                    )
                                                }
                                            >
                                                <Image
                                                    src={"https://image.tmdb.org/t/p/original/" + (poster.filePath)}/>
                                            </SwiperSlide>
                                        ))}
                                    </Swiper>
                                </TabPanel>
                                <TabPanel>
                                    <Swiper
                                        navigation={true}
                                        modules={[Navigation]}
                                        className="mySwiper"
                                        slidesPerView={4}
                                        pagination={{clickable: true}}
                                        spaceBetween={5}
                                    >
                                        {movie?.backdrops?.filter(b => b.lang === "en").map((backdrop) => (
                                            <SwiperSlide
                                                onClick={() =>
                                                    handleImageClick(
                                                        getPosterImageUri(
                                                            backdrop?.filePath!
                                                        )
                                                    )
                                                }
                                            >
                                                <Image src={getPosterImageUri(backdrop?.filePath!)}></Image>
                                            </SwiperSlide>
                                        ))}
                                    </Swiper>
                                </TabPanel>
                            </TabPanels>
                        </Tabs>
                    </GridItem>

                    {/*REVIEWS*/}
                    <GridItem colSpan={5} rowSpan={1}>
                        <MovieDetailsReviewsComponent
                            setReviews={setReviews}
                            movieId={movie?.id ?? 0}
                            reviews={reviews}
                            authenticatedUser={authenticatedUser}
                        />
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default MovieDetailsPage;
