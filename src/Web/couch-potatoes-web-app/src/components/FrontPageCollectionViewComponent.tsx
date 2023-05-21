import {Box, Button, Card, CardBody, Flex, Heading, Image, Stack, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import React, {FC} from "react";
import MovieCredits from "../models/movie_credits";
import MovieRecommendations from "../models/movie-Recommedations";


export interface MovieCollectionComponentProps {
    movieCollection: MovieRecommendations,
}

export const FrontPageCollectionViewComponent: FC<MovieCollectionComponentProps> = ({movieCollection}) => {
    return (
        <>

            <Flex gap={2} overflowX="auto" maxWidth={1500}>
                {movieCollection && movieCollection?.collection.map((movie) => (
                    <Box>
                        <Image maxWidth={225}
                               src={getPosterImageUri(movie?.imageUri as string)}
                               alt='No image available of actor'
                               borderRadius='lg'
                        />
                        <Button margin={0.5}  size='xs' key={movie.id}>
                            {movie.title}
                        </Button>
                    </Box>

                ))}

                </Flex>
                    </>
                    )
                }