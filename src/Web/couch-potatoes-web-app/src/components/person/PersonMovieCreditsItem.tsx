import React, {useState, useEffect, FC} from 'react';
import {Box, Card, CardBody, Image, Stack, StackDivider, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";

interface PersonMovieCreditsProperties {
    imageUri?: string;
    movieTitle?: string;
}

const PersonMovieCreditsItem: FC<PersonMovieCreditsProperties> = ({imageUri, movieTitle}) => {
    return (
        <Card maxW='sm' minWidth={200} maxWidth={400} minHeight={400} backgroundColor='transparent'>
            <CardBody _hover={{cursor: "pointer"}}>
                <Stack spacing='4'>
                    <Image
                        src={imageUri} alt={movieTitle}

                        height="auto"
                        objectFit="contain"/>

                    <Box>
                        <Text color="white" align='center'>{movieTitle}</Text>
                    </Box>
                </Stack>

            </CardBody>
        </Card>
    )
}

export default PersonMovieCreditsItem;