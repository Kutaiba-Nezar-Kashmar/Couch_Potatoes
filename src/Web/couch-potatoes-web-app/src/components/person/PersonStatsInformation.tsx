import React, {FC, ReactElement, useEffect, useState} from 'react';
import {Box, Grid, GridItem, Heading, HStack, Text, VStack} from "@chakra-ui/react";
import {sliceNumber} from "../../util/numberutil";
import {StarIcon} from "@chakra-ui/icons";

interface Stats {
    numberOfMovies?: number;
    averageMoviesRatingsAsACast?: number;
    averageMoviesRatingsAsACrew?: number;
    knownForGenre?: string;
}

const PersonStatsInformation: FC<Stats> = ({
                                               numberOfMovies,
                                               averageMoviesRatingsAsACast,
                                               averageMoviesRatingsAsACrew,
                                               knownForGenre
                                           }) => {
    return (
        <>
            <Heading marginBottom={"30px"} color="white" as='h4' size='md'>Statistics</Heading>

            <Grid templateColumns='repeat(4, 1fr)' gap={6}>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Total Number of movies</Heading>
                        </Box>
                        <Box>
                            {numberOfMovies ? (<Text color="white">{numberOfMovies}</Text>) : (<Text color="white">N/A</Text>)}
                        </Box>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Ratings as Actor</Heading>
                        </Box>
                        <HStack spacing={2}>
                            <StarIcon color='yellow'/>

                            {averageMoviesRatingsAsACast ? (sliceNumber(averageMoviesRatingsAsACast ?? 0, 1)) : (<Text color="white">N/A</Text>)}
                        </HStack>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Ratings for production</Heading>
                        </Box>
                        <HStack spacing={2}>
                            <StarIcon color='yellow'/>
                            {averageMoviesRatingsAsACrew ? (sliceNumber(averageMoviesRatingsAsACrew ?? 0, 1)) : (<Text color="white">N/A</Text>)}
                        </HStack>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Best know for</Heading>
                        </Box>
                        <Box>
                            {knownForGenre ? (<Text color="white">{knownForGenre}</Text>) : (<Text color="white">N/A</Text>)}
                        </Box>
                    </VStack>
                </GridItem>
            </Grid>
        </>
    )
}

export default PersonStatsInformation;