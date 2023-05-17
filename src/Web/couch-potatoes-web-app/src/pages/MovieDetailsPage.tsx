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
    ModalBody, Image
} from "@chakra-ui/react";
import StarRatingComponent from 'react-star-rating-component';
import Movie from "../models/movie";
import {SearchIcon} from "@chakra-ui/icons";
import {navBarHeightInRem, pageHPaddingInRem} from "../components/settings/page-settings";
import BackgroundImageFull from "../components/BackgroundImageFull";
import {useFetchCollections, useFetchPopularMovies} from "../services/movie-collection";
import {getPosterImageUri} from "../services/images";
import FrontPageMovieInfoBoxComponent from "../components/FrontPageMovieInfoBoxComponent";
import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import {Carousel} from 'react-responsive-carousel';

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

    const carouselMaxWidth = useBreakpointValue({base: "100%", md: "500px"});
    const navigate = useNavigate();
    const TEST_PAGE_URL = '/test';
    const [movie, setMovie] = useState<Movie | null>(null);
    const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

    const {isLoading, isError, data: collections, error} = useFetchCollections();

    // NOTE: (mibui 2023-05-15) Notice we use useEffect here to subscribe to the loading state of the useQuery hook.
    //                          In the dependency array we put isLoading to ensure that the page rerenders everytime
    //                          we are fetching data. Under the hood we are subscribing to some event emitter that
    //                          react-query provides.
    useEffect(() => {
        if (!isLoading) {
            // NOTE: (mibui 2023-05-15) Take the most popular movie as featured.
            // TODO: (mibui 2023-05-15) Fetch from /movies/:movieid instead of taking from collection.
            //                          e.g. setMovie(getMovie((collections as any)!["popular"]["collection"][0]?.id))
            setMovie({
                ...(collections as any)!["popular"]["collection"][0],
                genres: [{name: "Horror", id: 1}, {name: "Thriller", id: 2}, {name: "Romance", id: 3}]
            }); // TODO: Remove the genres, they are only there for testing purposes
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

    if (collections) {
        console.log(collections);
    }

    return (
        <BackgroundImageFull imageUri={getPosterImageUri(movie?.imageUri as string) || Background_Temp}>
            <BasePage>
                <Grid
                    templateColumns={{base: "1fr", md: "repeat(1, 1fr)", lg: "repeat(1, 1fr)"}}
                    gap={4}
                >
                    {/* TITEL AND RATING */}
                    <GridItem colSpan={1}>
                        <Stack direction="row" justify="space-between">
                            <Box bg="yellow">
                                <Text fontSize="3xl">
                                    Movie Titel
                                </Text> </Box>
                            <Box bg="tomato">
                                <Text fontSize="1xl">
                                    Rating: Trash
                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>

                    {/* YEAR AND RUNTIME */}
                    <GridItem>
                        <Stack direction="row" divider={<StackDivider borderColor='gray.200'/>}>
                            <Box bg="yellow">
                                <Text fontSize="lg">
                                    2023
                                </Text> </Box>
                            <Box bg="tomato">
                                <Text fontSize="lg">
                                    2h 21m
                                </Text>
                            </Box>
                        </Stack>
                    </GridItem>
                    {/* CAROUSEL */}
                    <GridItem bg="tomato">
                        <Box maxWidth={carouselMaxWidth} mx="auto">
                            <Carousel  infiniteLoop={true} autoPlay={true} interval={3000}
                                      stopOnHover={true}>
                                <Box
                                    onClick={() => handleImageClick("https://www.alleycat.org/wp-content/uploads/2019/03/FELV-cat.jpg")}>
                                    <Image boxSize="300px" objectFit="cover"
                                           src="https://www.alleycat.org/wp-content/uploads/2019/03/FELV-cat.jpg"/>
                                    <p className="legend">Kutaiba</p>
                                </Box>
                                <Box
                                    onClick={() => handleImageClick("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Cat03.jpg/1200px-Cat03.jpg")}>
                                    <Image boxSize="300px" objectFit="cover"
                                           src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Cat03.jpg/1200px-Cat03.jpg"/>
                                    <p className="legend">Michael</p>
                                </Box>
                                <Box
                                    onClick={() => handleImageClick("https://idsb.tmgrup.com.tr/ly/uploads/images/2021/09/08/142774.jpg")}>
                                    <Image boxSize="300px" objectFit="cover"
                                           src="https://idsb.tmgrup.com.tr/ly/uploads/images/2021/09/08/142774.jpg"/>
                                    <p className="legend">Kasper</p>
                                </Box>
                            </Carousel>
                            <Modal isOpen={isOpen} onClose={handleModalClose} size="4xl">
                                <ModalOverlay/>
                                <ModalContent>
                                    <ModalCloseButton/>
                                    <ModalBody justifyContent="center">
                                        <Image src={expandedImage} alt="Expanded Image" mx="auto" maxHeight={500}/>
                                    </ModalBody>
                                </ModalContent>
                            </Modal>
                        </Box>

                    </GridItem>

                    {/* INFORMATION */}
                    <GridItem>
                        <Stack direction="column" divider={<StackDivider borderColor='gray.200'/>}>
                            <Box bg="yellow">
                                <Text fontSize="2xl">
                                    Information
                                </Text> </Box>
                            <Box bg="tomato">
                                <Stack direction="row">
                                    <Text fontSize="lg" as="b">Summary:</Text>
                                    <Text fontSize="lg">
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent et
                                        tincidunt turpis, non cursus massa. Sed sapien elit, consectetur ut risus eget,
                                        pulvinar ultricies justo. Nullam aliquam maximus bibendum. Fusce auctor nulla
                                        maximus viverra gravida. Aliquam vitae viverra nisl. Sed justo justo, pretium et
                                        vehicula at, imperdiet sit amet tellus. Morbi porta tellus sit amet ligula
                                        dictum, vitae scelerisque lorem mattis. Nullam ultricies diam interdum sapien
                                        faucibus tempus. Interdum et malesuada fames ac ante ipsum primis in faucibus.
                                        Vestibulum dapibus pharetra ante, eget aliquet quam vehicula a.
                                    </Text>
                                </Stack>
                                <Stack direction="row">
                                    <Text fontSize="lg" as="b">Run time:</Text>
                                    <Text fontSize="lg">
                                        3h 20 min
                                    </Text>
                                </Stack>
                                <Stack direction="row">
                                    <Text fontSize="lg" as="b">Release Date:</Text>
                                    <Text fontSize="lg">
                                        2023/03/94
                                    </Text>
                                </Stack>
                                <Stack direction="row">
                                    <Text fontSize="lg" as="b">Kid safe:</Text>
                                    <Text fontSize="lg">
                                        Hell naw'
                                    </Text>
                                </Stack>

                            </Box>
                        </Stack>
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default MovieDetailsPage