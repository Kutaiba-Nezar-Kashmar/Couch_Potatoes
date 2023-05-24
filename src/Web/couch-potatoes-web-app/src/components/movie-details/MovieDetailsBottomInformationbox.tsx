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
import { useNavigate } from 'react-router-dom';

export interface MovieDetailsComponentProps {
    movie: Movie | null;
    themeColor: string;
}

export interface MovieDetailsCreditsComponentProps {
    movieCredits: MovieCredits | null;
    movie: Movie | null;
    themeColor: string;
}

export const MovieDetailsBottomInformationbox: FC<
    MovieDetailsCreditsComponentProps
> = ({ movieCredits, movie, themeColor }) => {
    const navigate = useNavigate();
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
                                {movie?.summary ? movie.summary : 'N/A'}
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
                                                        onClick={() =>
                                                            navigate(
                                                                `/Couch_Potatoes/person/${crew.id}`
                                                            )
                                                        }
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
                                                        onClick={() =>
                                                            navigate(
                                                                `/Couch_Potatoes/person/${crew.id}`
                                                            )
                                                        }
                                                        _hover={{
                                                            color: 'grey',
                                                        }}
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
                                        onClick={() =>
                                            navigate(
                                                `/Couch_Potatoes/movie/${movie?.id}/cast`
                                            )
                                        }
                                        target="_blank"
                                        rel="noopener noreferrer"
                                    >
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
