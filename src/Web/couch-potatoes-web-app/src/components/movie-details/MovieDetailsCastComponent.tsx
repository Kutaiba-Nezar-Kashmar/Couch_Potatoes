import {
    Card,
    CardBody,
    CardHeader,
    Flex,
    Heading,
    Image,
    Stack,
    Text,
} from '@chakra-ui/react';
import { getPosterImageUri } from '../../services/images';
import React, { FC } from 'react';
import MovieCredits from '../../models/movie_credits';
import Movie from '../../models/movie';
import { useNavigate } from 'react-router-dom';

export interface MovieDetailsCastComponentProps {
    movieCredits: MovieCredits;
    Background_Temp: string;
    themeColor: string;
}

export const MovieDetailsCastComponent: FC<MovieDetailsCastComponentProps> = ({
    movieCredits,
    Background_Temp,
    themeColor,
}) => {
    const navigate = useNavigate();const scrollbarStyles = {
        width: "8px",
        backgroundColor: "white",
        borderRadius: "4px",
    };

    const hoverStyles = {
        backgroundColor: "darkgray",
    };
    return (
        <>
            <Card>
                <CardHeader>
                    <Heading size="md">Cast</Heading>
                </CardHeader>
                <CardBody>
                    <Flex gap={2} overflowX="auto"  sx={{
                        "&::-webkit-scrollbar": scrollbarStyles,
                        "&::-webkit-scrollbar-thumb": hoverStyles,
                        "&::-webkit-scrollbar-thumb:hover": hoverStyles,
                    }}>
                        {movieCredits && movieCredits?.creditsAsCast.length>0 ?(movieCredits &&
                            movieCredits?.creditsAsCast.slice(0,10).map((cast) => (
                                <Card maxW="sm" minWidth={200} maxWidth={400}>
                                    <CardBody
                                        _hover={{ cursor: 'pointer' }}
                                        onClick={() =>
                                            navigate(
                                                `/Couch_Potatoes/person/${cast.id}`
                                            )
                                        }
                                    >
                                        {cast?.profilePath ? (
                                            <Image
                                                src={getPosterImageUri(
                                                    cast?.profilePath
                                                )}
                                                alt="Profile picture of actor"
                                                borderRadius="lg"
                                            />
                                        ) : (
                                            <Image
                                                minHeight={225}
                                                src={Background_Temp}
                                                alt="No image available of actor"
                                                borderRadius="lg"
                                            />
                                        )}

                                        <Stack mt="3" direction="column">
                                            <Heading size="sm">
                                                {' '}
                                                {cast?.name}
                                            </Heading>
                                            <Text
                                                fontStyle="italic"
                                                color={themeColor}
                                                fontSize="md"
                                            >
                                                {cast?.character as string}
                                            </Text>
                                        </Stack>
                                    </CardBody>
                                </Card>
                            ))):("N/A")}

                    </Flex>
                </CardBody>
            </Card>
        </>
    );
};
