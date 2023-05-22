import React, {FC, ReactElement, useEffect, useState} from 'react';
import {Box, Grid, GridItem, Heading, Text, VStack} from "@chakra-ui/react";

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
            <Heading color="white" as='h4' size='md'>Stats</Heading>
            <Grid templateColumns='repeat(4, 1fr)' gap={6}>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Total Number of movies</Heading>
                        </Box>
                        <Box>
                            <Text color="white">{numberOfMovies}</Text>
                        </Box>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Ratings as Actor</Heading>
                        </Box>
                        <Box>
                            <Text color="white">{averageMoviesRatingsAsACast}</Text>
                        </Box>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Ratings for production</Heading>
                        </Box>
                        <Box>
                            <Text color="white">{averageMoviesRatingsAsACrew}</Text>
                        </Box>
                    </VStack>
                </GridItem>
                <GridItem colSpan={1}>
                    <VStack spacing={2}>
                        <Box>
                            <Heading color="white" as='h3' size='sm'>Best know for working with</Heading>
                        </Box>
                        <Box>
                            <Text color="white">{knownForGenre}</Text>
                        </Box>
                    </VStack>
                </GridItem>
            </Grid>
        </>
    )
}

export default PersonStatsInformation;