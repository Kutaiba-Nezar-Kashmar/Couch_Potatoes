import Movie from '../../models/movie';
import React, { FC } from 'react';
import {
    Box,
    Button,
    Card,
    CardBody,
    CardHeader,
    Heading,
    Link,
    Stack,
    StackDivider,
    Text,
} from '@chakra-ui/react';
import MovieCredits from '../../models/movie_credits';

export interface MovieDetailsCreditsComponentProps {
    movieCredits: MovieCredits | null;
    movie: Movie | null;
    themeColor: string;
}

export const MovieDetailsBottomInformationbox: FC<
    MovieDetailsCreditsComponentProps
> = ({ movieCredits, movie, themeColor }) => {
    return (
        <>
            <Card>
                <CardHeader>
                    <Heading size="md">Information</Heading>
                </CardHeader>
                <CardBody>
                    <Stack divider={<StackDivider />} spacing="2">
                        <Box>
                            <Heading textTransform="uppercase" size="sm">
                                Summary
                            </Heading>
                            <Text pt="2" fontSize="sm" marginBottom={5}>
                                {movie?.summary}
                            </Text>
                            <Stack direction="row" spacing="8">
                                <Stack>
                                    <Heading
                                        textTransform="uppercase"
                                        size="sm"
                                    >
                                        Director(s)
                                    </Heading>
                                    <Stack>
                                        {movieCredits &&
                                            movieCredits?.creditsAsCrew
                                                .filter(
                                                    (crew) =>
                                                        crew.job === 'Director'
                                                )
                                                .map((crew) => (
                                                    <Link
                                                        color={themeColor}
                                                        _hover={{
                                                            color: 'grey',
                                                        }}
                                                        href="https://example.com"
                                                        target="_blank"
                                                        rel="noopener noreferrer"
                                                    >
                                                        {' '}
                                                        {crew.name}
                                                    </Link>
                                                ))}
                                    </Stack>
                                </Stack>

                                <Stack>
                                    <Heading
                                        textTransform="uppercase"
                                        size="sm"
                                    >
                                        Screenplay
                                    </Heading>
                                    <Stack>
                                        {movieCredits &&
                                            movieCredits?.creditsAsCrew
                                                .filter(
                                                    (crew) =>
                                                        crew.job ===
                                                        'Screenplay'
                                                )
                                                .map((crew) => (
                                                    <Link
                                                        color={themeColor}
                                                        _hover={{
                                                            color: 'grey',
                                                        }}
                                                        href="https://example.com"
                                                        target="_blank"
                                                        rel="noopener noreferrer"
                                                    >
                                                        {' '}
                                                        {crew.name}
                                                    </Link>
                                                ))}
                                    </Stack>
                                </Stack>
                                <Text
                                    padding={0}
                                    textTransform="uppercase"
                                    size="sm"
                                >
                                    <Link
                                        color={themeColor}
                                        _hover={{ color: 'grey' }}
                                        href="https://example.com"
                                        target="_blank"
                                        rel="noopener noreferrer"
                                    >
                                        {/* TODO: change href to list of cast and crew  */}
                                        Full list of cast and crew
                                    </Link>
                                </Text>
                            </Stack>
                        </Box>

                        <Box>
                            <Heading
                                marginBottom={3}
                                size="sm"
                                textTransform="uppercase"
                            >
                                Genres
                            </Heading>
                            {movie &&
                                movie?.genres.map((genre) => (
                                    <Button
                                        margin={0.5}
                                        colorScheme={themeColor}
                                        size="xs"
                                        key={genre.id}
                                        className="genre"
                                    >
                                        {genre.name}
                                    </Button>
                                ))}
                        </Box>
                    </Stack>
                </CardBody>
            </Card>
        </>
    );
};
