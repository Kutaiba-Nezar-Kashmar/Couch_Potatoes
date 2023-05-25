import {Card, CardBody, CardHeader, Flex, Heading, Image, Stack, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";
import React, {FC} from "react";
import MovieCredits from "../../models/movie_credits";
import Movie from "../../models/movie";
import MovieRecommendations from "../../models/movie-Recommedations";
import starIcon from "../../assets/iconstar.png";
import {sliceNumber} from "../../util/numberutil";
import {useNavigate} from 'react-router-dom';

export interface MovieDetailsRecommendMoviesComponentProps {
    movieRecommendations: MovieRecommendations | null;
    Background_Temp: string;
    themeColor: string;
}


export const MovieDetailsRecommendedMoviesComponent: FC<MovieDetailsRecommendMoviesComponentProps> = ({
                                                                                                          movieRecommendations,
                                                                                                          Background_Temp,
                                                                                                          themeColor
                                                                                                      }) => {
    const navigate = useNavigate();
    return (
        <>

            <Card>
                <CardHeader>
                    <Heading size='md'>Recommended Movies</Heading>
                </CardHeader>
                <CardBody>
                    <Flex gap={2} overflowX="auto">
                        {movieRecommendations && movieRecommendations?.collection.map((movie) => (
                            <Card maxW='sm' minWidth={200} maxWidth={400}>
                                <CardBody onClick={() =>
                                    navigate(
                                        `/Couch_Potatoes/movie/details/${movie.id}`
                                    )
                                }
                                          _hover={{cursor: "pointer"}}>
                                    {(movie?.imageUri) ? (<Image
                                        src={getPosterImageUri(movie?.imageUri)}
                                        alt={"poster of movie" + movie.title}
                                        borderRadius='lg'
                                    />) : (<Image minHeight={225}
                                                  src={Background_Temp}
                                                  alt={"no poster of movie" + movie.title}
                                                  borderRadius='lg'
                                    />)}

                                    <Stack mt='3' direction="column">
                                        <Heading size='sm'> {movie?.title}</Heading>
                                        <Stack justifyContent="start" alignItems="center" direction="row">
                                            <Image maxHeight={3}
                                                   src={starIcon}/>
                                            <Text fontStyle="italic" color={themeColor} fontSize='sm'>

                                                {sliceNumber(movie?.tmdbScore, 1)}/10
                                            </Text>

                                        </Stack>

                                    </Stack>
                                </CardBody>
                            </Card>
                        ))}
                    </Flex>


                </CardBody>
            </Card>

        </>
    )
}