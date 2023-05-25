import React, {FC} from "react";
import {Box, Button, Card, CardBody, CardHeader, Heading, Link, Stack, StackDivider, Text} from "@chakra-ui/react";
import Movie from "../../models/movie";

export interface MovieDetailsRightInformationBoxProps {
    movie: Movie | null;
    themeColor: string;
}

export const MovieDetailsRightInformationbox: FC<MovieDetailsRightInformationBoxProps> = ({movie, themeColor}) => {
    return (
        <>

            <Card>
                <CardHeader>
                    <Heading size='md'>Quick facts</Heading>
                </CardHeader>

                <CardBody>
                    <Stack divider={<StackDivider/>} spacing='4'>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                Runtime
                            </Heading>
                            <Text pt='2' fontSize='sm'>
                                {movie && movie?.runTime > 0 ? (movie.runTime + " minutes") : ("N/A")}

                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                Release Date
                            </Heading>
                            <Text pt='2' fontSize='sm'>
                                {movie?.releaseDate ? (movie.releaseDate.slice(0, 10)) : ("N/A")}
                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                kid friendly
                            </Heading>
                            <Text pt='2' fontSize='sm'>
                                {movie?.isForKids ? (movie?.isForKids ? "Yes" : "No") : ("N/A")}
                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                status
                            </Heading>
                            <Text pt='2' fontSize='sm'>
                                {movie?.status ? (movie.status) : ("N/A")}
                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                home page
                            </Heading>
                            <Link pt='2' fontSize='sm' href={movie?.homepage}
                                  color={themeColor}

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
                                {movie && movie?.budget > 0 ? ("$" + movie?.budget.toLocaleString()) : ("N/A")}

                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                Revenue
                            </Heading>
                            <Text pt='2' fontSize='sm'>

                                {movie && movie?.revenue > 0 ? ("$" + movie?.revenue.toLocaleString()) : ("N/A")}
                            </Text>
                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                Keywords
                            </Heading>
                            {movie?.keywords ? (movie && movie?.keywords.map((keyword) => (
                                <Button margin={0.5} colorScheme={themeColor} size='xs' key={keyword.id}
                                        className="keyword-box">
                                    {keyword.name}
                                </Button>
                            ))) : ("N/A")}

                        </Box>
                        <Box>
                            <Heading size='xs' textTransform='uppercase'>
                                spoken languages
                            </Heading>

                            {movie?.languages ? (movie && movie?.languages.map((language) => (
                                <Button margin={0.5} colorScheme={themeColor} size='xs' key={language.code}
                                        className="keyword-box">
                                    {language.name}
                                </Button>
                            ))) : ("N/A")}
                        </Box>
                    </Stack>
                </CardBody>
            </Card>

        </>
    )
}