import React, {FC, useEffect, useState} from 'react';
import {Box, Card, CardBody, Heading, HStack, Text, VStack} from "@chakra-ui/react";
import {useNavigate} from "react-router-dom";

interface CastDetails {
    title?: string;
    releaseDate?: Date;
    character?: string;
    movieId : number
}

const CreditsAsCastDetails: FC<CastDetails> = ({title, releaseDate, character,movieId}) => {
    const navigate = useNavigate();
    return (
        <Card   _hover={{cursor: "pointer"}} backgroundColor='white'  onClick={() =>
            navigate(
                `/Couch_Potatoes/movie/details/${movieId}`
            )
        }>
            <CardBody>
                <HStack spacing={100}>
                    <Box>
                        <Text>{releaseDate?.getFullYear() ?? "-"}</Text>
                    </Box>
                    <VStack spacing={2} display='flex' alignItems='left'>
                        <Box>
                            <Heading as='h4' size='sm'>{title ?? "UNKNOWN"}</Heading>
                        </Box>
                        <Box>
                            <Text>as: {character ?? "-"}</Text>
                        </Box>
                    </VStack>
                </HStack>
            </CardBody>
        </Card>
    )
}

export default CreditsAsCastDetails;