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
    ModalBody, Image, Card, CardHeader, Heading, CardBody, Link
} from "@chakra-ui/react";
import Movie from "../models/movie";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {getPosterImageUri} from "../services/images";
import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import {Carousel} from 'react-responsive-carousel';
import {useFetchMovieDetails} from "../services/movie-details";
import {Swiper, SwiperSlide} from "swiper/react";
import {Autoplay, Navigation, Pagination} from "swiper";
import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/navigation";
import MovieCredits from "../models/movie_credits";
import {useFetchMovieCredits} from "../services/movie-credits";
import {MovieCreditsAndDetails, useFetchMovieCreditsAndMovies} from "../services/movie-credits-and-details";

const MovieDetailsPage = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [expandedImage, setExpandedImage] = useState("");

    const handleImageClick = (imageUrl: string) => {
        setExpandedImage(imageUrl);
        setIsOpen(true);
    };

    const handleModalClose = () => {
        setExpandedImage("");
        setIsOpen(false);
    };

    const carouselMaxWidth = useBreakpointValue({base: "100%", md: "1000px"});
    const navigate = useNavigate();

    const [movie, setMovie] = useState<Movie | null>(null);
    const [movieCredits, setMovieCredits] = useState<MovieCredits | null>(null);

    const {isLoading, isError, data, error} = useFetchMovieCreditsAndMovies(8587);


    function convertToHoursAndMinutes(num: number) {
        const hours = Math.floor(num / 60); // Get the number of hours
        const minutes = num % 60; // Get the remaining minutes

        return `${hours}h ${minutes}m`; // Return the formatted string
    }

    useEffect(() => {
        if (!isLoading) {
            const detailsAndCredits = data as MovieCreditsAndDetails
            setMovie(
               data?.movieDetails?? null

            );
            setMovieCredits(
                data?.credits?? null

            );
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

        <BackgroundImageFull imageUri={getPosterImageUri(movie?.backdropUri as string)}>
            <BasePage>
                <Grid
                    templateColumns={{base: "6fr", md: "repeat(6, 1fr)", lg: "repeat(6, 1fr)"}}

                    gap={4}
                >
                    {/* TITEL AND RATING */}
                    <GridItem colSpan={6} rowSpan={1}>
                        <Stack direction="row" justify="space-between">
                            <Stack direction="column" divider={<StackDivider borderColor='gray.200'/>}>
                                <Stack direction="row">
                                    <Box>
                                        <Text color={"white"} fontSize="3xl">
                                            {movie?.title}
                                        </Text>
                                    </Box>

                                    {movieCredits && movieCredits?.creditsAsCrew.filter((crew)=>(crew.job==="Sculptor")).map((cast) => (
                                        <Button margin={0.5} colorScheme='teal' size='xs' key={cast.id}
                                                className="keyword-box">
                                            {cast.name}
                                        </Button>
                                    ))}
                                    <Flex overflowX="auto">
                                        <Box display="flex">
                                            {/* Render your card elements here */}
                                            <Box bg="blue.200" width="200px" height="200px" m="4">
                                                Card 1
                                            </Box>
                                            <Box bg="green.200" width="200px" height="200px" m="4">
                                                Card 2
                                            </Box>
                                            <Box bg="red.200" width="200px" height="200px" m="4">
                                                Card 3
                                            </Box>
                                            {/* Add more card elements as needed */}
                                        </Box>
                                    </Flex>
                                    <Box>
                                        <Text color={"gray"} fontSize="3xl">
                                            ({movie?.releaseDate.slice(0, 4)})
                                        </Text>
                                    </Box>

                                </Stack>


                                <Box>
                                    <Text fontStyle="italic" color={"white"} fontSize="2l">
                                        {movie?.tagLine}
                                    </Text>
                                </Box>
                            </Stack>


                            <Box>
                                <Text color={"white"} fontSize="2xl">
                                    Rating: {movie?.tmdbScore}/10
                                    {/* YEAR AND RUNTIME */}

                                </Text>
                                <Text color={"white"} fontSize="1xl" align="end">
                                    Vote#: {movie?.tmdbVoteCount} dummy
                                    {/* TODO: This does not work */}
                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>

                    {/*  YEAR AND RUNTIME */}
                    <GridItem colSpan={6}
                              rowSpan={1}>
                        <Stack direction="row" divider={<StackDivider borderColor='gray.200'/>}>




                            <Box>
                                <Text color={"white"} fontSize="lg">
                                    {convertToHoursAndMinutes(movie?.runTime as number)}
                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>
                    {/* CAROUSEL */}
                    <GridItem colSpan={5} rowSpan={4}>
                        <Box>
                            <Carousel infiniteLoop={true} autoPlay={true} interval={3000}
                                      stopOnHover={true} dynamicHeight={true}>
                                <Box _hover={{cursor: "pointer"}}
                                     onClick={() => handleImageClick(getPosterImageUri(movie?.imageUri as string))}>
                                    <img style={{
                                        width: "100%",
                                        maxHeight: "500px",
                                        height: "auto",
                                        objectFit: "contain"
                                    }}
                                         src={getPosterImageUri(movie?.imageUri as string)}/>
                                </Box>
                                <Box _hover={{cursor: "pointer"}}
                                     onClick={() => handleImageClick(getPosterImageUri(movie?.backdropUri as string))}>
                                    <img style={{
                                        width: "100%",
                                        maxHeight: "500px",
                                        height: "auto",
                                        objectFit: "contain"
                                    }}
                                         src={getPosterImageUri(movie?.backdropUri as string)}/>
                                </Box>

                            </Carousel>
                            <Modal isOpen={isOpen} onClose={handleModalClose} size="6xl">
                                <ModalOverlay/>
                                <ModalContent>
                                    <ModalCloseButton/>
                                    <ModalBody justifyContent="center">
                                        <Image src={expandedImage} alt="Expanded Image" mx="auto" maxHeight={750}/>
                                    </ModalBody>
                                </ModalContent>
                            </Modal>
                        </Box>

                    </GridItem>

                    {/* INFORMATIONBAR RIGHT */}
                    <GridItem rowSpan={6} colSpan={1}>
                        <Card>
                            <CardHeader>
                                <Heading size='md'>Quick facts or something</Heading>
                            </CardHeader>

                            <CardBody>
                                <Stack divider={<StackDivider/>} spacing='4'>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Runtime
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            {movie?.runTime} minutes
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Release Date
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            {movie?.releaseDate.slice(0, 10)}
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            kid friendly
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            {movie?.isForKids ? "Yes" : "No"}
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            status
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            {movie?.status}
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            home page
                                        </Heading>
                                        <Link pt='2' fontSize='sm' href={movie?.homepage}
                                              color="blue.500"

                                              _hover={{color: "blue.700"}}
                                              _focus={{outline: "none", boxShadow: "outline"}}
                                              _active={{color: "blue.700"}}
                                              target="_blank"
                                              rel="noopener noreferrer">
                                            Link to homepage
                                        </Link>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Budget
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            ${movie?.budget.toLocaleString()}
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Revenue
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            ${movie?.revenue.toLocaleString()}
                                        </Text>
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Keywords
                                        </Heading>

                                        {movie && movie?.keywords.map((keyword) => (
                                            <Button margin={0.5} colorScheme='teal' size='xs' key={keyword.id}
                                                    className="keyword-box">
                                                {keyword.name}
                                            </Button>
                                        ))}
                                    </Box>
                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            spoken languages
                                        </Heading>
                                        {movie && movie?.languages.map((language) => (
                                            <Button margin={0.5} colorScheme='teal' size='xs' key={language.code}
                                                    className="keyword-box">
                                                {language.name}
                                            </Button>
                                        ))}
                                    </Box>
                                </Stack>
                            </CardBody>
                        </Card>
                    </GridItem>

                    {/* INFORMATIONBAR BOTTOM */}

                    <GridItem colSpan={5} rowSpan={1}>
                        <Card>
                            <CardHeader>
                                <Heading size='md'>Summary</Heading>
                            </CardHeader>
                            <CardBody>
                                <Stack divider={<StackDivider/>} spacing='4'>
                                    <Box>

                                        <Text pt='2' fontSize='sm'>
                                            {movie?.summary}
                                        </Text>
                                    </Box>

                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                          Genres
                                        </Heading>
                                        {movie && movie?.genres.map((genre) => (
                                            <Button margin={0.5} colorScheme='teal' size='xs' key={genre.id}
                                                    className="genre">
                                                {genre.name}
                                            </Button>
                                        ))}
                                    </Box>


                                </Stack>
                            </CardBody>
                        </Card>
                    </GridItem>

                    <GridItem colSpan={5} >
                        <Card>
                            <CardHeader>
                                <Heading size='md'>Cast</Heading>
                            </CardHeader>

                            <CardBody>
                                <Stack divider={<StackDivider/>} spacing='4'>

                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Analysis
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                          UwU
                                        </Text>
                                    </Box>
                                </Stack>
                            </CardBody>
                        </Card>
                    </GridItem>

                    <GridItem colSpan={5} rowSpan={1}>
                        <Card>
                            <CardHeader>
                                <Heading size='md'>Reviews</Heading>
                            </CardHeader>

                            <CardBody>
                                <Stack divider={<StackDivider/>} spacing='4'>

                                    <Box>
                                        <Heading size='xs' textTransform='uppercase'>
                                            Analysis
                                        </Heading>
                                        <Text pt='2' fontSize='sm'>
                                            UwU
                                        </Text>
                                    </Box>
                                </Stack>
                            </CardBody>
                        </Card>
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>

    );
};

export default MovieDetailsPage