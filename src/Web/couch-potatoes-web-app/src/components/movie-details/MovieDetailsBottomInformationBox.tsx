import Movie from '../../models/movie';
import React, {FC} from 'react';
import {
    Box,
    Button,
    Card,
    CardBody,
    CardHeader,
    Heading, Image,
    Link,
    Stack,
    StackDivider,
    Text,
} from '@chakra-ui/react';
import MovieCredits from '../../models/movie_credits';
import {useNavigate} from 'react-router-dom';
import {getPosterImageUri} from "../../services/images";


export interface MovieDetailsCreditsComponentProps {
    movieCredits: MovieCredits | null;
    movie: Movie | null;
    themeColor: string;
}

export const MovieDetailsBottomInformationBox: FC<
    MovieDetailsCreditsComponentProps
> = ({movieCredits, movie, themeColor}) => {
    const navigate = useNavigate();
    return (
        <>
            <Card>
                <CardHeader>
                    <Heading size="md">Information</Heading>
                </CardHeader>
                <CardBody>
                    <Stack divider={<StackDivider/>} spacing="2">
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
                                        {movieCredits && movieCredits?.creditsAsCrew?.length > 0 ? (movieCredits &&
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
                                                ))) : (<Text>N/A</Text>)}

                                    </Stack>
                                </Stack>

                                <Stack>
                                    <Heading
                                        textTransform="uppercase"
                                        size="sm"
                                    >
                                        Producer(s)
                                    </Heading>
                                    <Stack>
                                        {movieCredits && movieCredits?.creditsAsCrew?.filter(c => c.job === 'Producer').length > 0 ? (movieCredits &&
                                            movieCredits?.creditsAsCrew
                                                .filter(
                                                    (crew) =>
                                                        crew.job ===
                                                        'Producer'
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
                                                ))) : (<Text> N/A</Text>)}

                                    </Stack>
                                </Stack>
                                <Text
                                    padding={0}
                                    textTransform="uppercase"
                                    size="sm"
                                >
                                    <Link
                                        color={themeColor}
                                        _hover={{color: 'grey'}}
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
                                <Stack  divider={<StackDivider borderColor="gray.200"/>} direction={"column"}>
                                    <Heading
                                        textTransform="uppercase"
                                        size="sm"
                                    >
                                        Production company(s)
                                    </Heading>

                                    {movie?.productionCompanies.map(pc => (
                                            <Stack  direction={"row"}>
                                                <Text color={themeColor}> {pc.name}</Text>
                                                {pc.logoPath ? (<Image maxHeight={5} src={getPosterImageUri(
                                                    pc?.logoPath
                                                )}></Image>):("")}

                                            </Stack>


                                        )
                                    )}


                                </Stack>
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
                            {movie && movie?.genres.length > 0 ? (movie &&
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
                                ))) : ("N/A")}

                        </Box>
                    </Stack>
                </CardBody>
            </Card>
        </>
    );
};
