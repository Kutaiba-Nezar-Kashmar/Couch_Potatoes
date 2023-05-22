import React, { FC, useState } from 'react';
import Movie from '../../models/movie';
import {
    Box,
    Card,
    CardBody,
    CardHeader,
    Flex,
    Heading,
    Image,
    Text,
    VStack,
} from '@chakra-ui/react';
import { getPosterImageUri } from '../../services/images';
import StarRatingComponent from 'react-star-rating-component';
import { groupElements } from '../../util/listutil';

export interface FavoriteMovieCardProps {
    movie: Movie;
    editing?: boolean;
}

const FavoriteMovieCard: FC<FavoriteMovieCardProps> = ({ movie, editing }) => {
    const [hovered, setHovered] = useState<boolean>(false);
    const [isEditing, setIsEditing] = useState<boolean>(editing ?? false);
    const [genres, setGenres] = useState(groupElements(movie.genres, 3)[0]);

    return (
        <Box
            padding="1.0rem 1.5rem"
            background="#ECEFF4"
            rounded="lg"
            minHeight={{ base: '400px' }}
            transition="all 0.2s ease-in-out"
            transform={hovered ? 'scale(1.05)' : 'scale(1.0)'}
            filter={hovered ? 'brightness(1.1)' : 'brightness(0.9)'}
            shadow={hovered ? 'dark-lg' : 'none'}
            cursor="pointer"
            width={{ base: '200px', md: '250px' }}
            onMouseEnter={() => setHovered(true)}
            onMouseLeave={() => setHovered(false)}
        >
            <Flex justify="center" alignItems="center" direction="column">
                <Text
                    marginBottom="1rem"
                    fontSize="sm"
                    as="b"
                    textTransform="uppercase"
                >
                    {movie.tagLine ? movie.tagLine : <em>N/A</em>}
                </Text>
                <Flex
                    width="100%"
                    height="100%"
                    justify="center"
                    align="center"
                    boxShadow="xl"
                    rounded="lg"
                    direction="column"
                    maxHeight={{ base: '150px', md: '200px' }}
                    maxWidth={{ base: '100px', md: '150px' }}
                >
                    <Image
                        rounded="lg"
                        height="100%"
                        width="100%"
                        src={getPosterImageUri(movie.imageUri)}
                    />
                </Flex>

                <Flex
                    width="100%"
                    height="100%"
                    flexDir="column"
                    justify="center"
                    alignItems="center"
                    marginTop="1rem"
                >
                    <Text textTransform="uppercase" textShadow="lg">
                        {movie.title}
                    </Text>
                    <StarRatingComponent
                        name="MovieRating"
                        value={movie.tmdbScore}
                        starCount={10}
                    />
                    <Text fontSize="sm">
                        {genres.map((genre) => genre.name).join(' | ')}
                    </Text>
                </Flex>
            </Flex>
        </Box>
    );
};

export default FavoriteMovieCard;
