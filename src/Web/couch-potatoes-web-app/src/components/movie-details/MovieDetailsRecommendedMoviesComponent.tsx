import {Card, CardBody, CardHeader, Flex, Heading, Image, Stack, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";
import React, {FC} from "react";
import MovieCredits from "../../models/movie_credits";
import Movie from "../../models/movie";
import MovieRecommendations from "../../models/movie-Recommedations";
import starIcon from "../../assets/iconstar.png";

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
                                <CardBody
                                    _hover={{cursor: "pointer"}}> {/*TODO: Need onClick event to movie page in cardbody*/}
                                    {(movie?.imageUri) ? (<Image
                                        src={getPosterImageUri(movie?.imageUri)}
                                        alt='Profile picture of actor'
                                        borderRadius='lg'
                                    />) : (<Image minHeight={225}
                                                  src={Background_Temp}
                                                  alt='No image available of actor'
                                                  borderRadius='lg'
                                    />)}

                                    <Stack mt='3' direction="column">
                                        <Heading size='sm'> {movie?.title}</Heading>
                                        <Stack justifyContent="start" alignItems="center"  direction="row">
                                            <Image maxHeight={3}
                                                   src={starIcon}/>
                                            <Text fontStyle="italic" color={themeColor} fontSize='sm'>

                                                {movie?.tmdbScore}/10
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