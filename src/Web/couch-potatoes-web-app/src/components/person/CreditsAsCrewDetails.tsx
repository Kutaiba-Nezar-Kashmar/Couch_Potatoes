import React, {FC, useEffect, useState} from 'react';
import {Box, Card, CardBody, Heading, HStack, Text, VStack} from "@chakra-ui/react";
import {useNavigate} from "react-router-dom";

interface CrewDetails {
    title?: string;
    department?: string;
    job?: string;
    releaseDate?: Date;
    movieId: number
}

const CreditsAsCrewDetails: FC<CrewDetails> = ({title, movieId, department, job, releaseDate}) => {
    const navigate = useNavigate();
    return (
        <Card _hover={{cursor: "pointer"}} onClick={() =>
            navigate(
                `/Couch_Potatoes/movie/details/${movieId}`
            )} backgroundColor='white'>
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
                            <Text>Department: {department}</Text>
                        </Box>
                        <Box>
                            <Text>as: {job}</Text>
                        </Box>
                    </VStack>
                </HStack>
            </CardBody>
        </Card>
    )
}

export default CreditsAsCrewDetails;