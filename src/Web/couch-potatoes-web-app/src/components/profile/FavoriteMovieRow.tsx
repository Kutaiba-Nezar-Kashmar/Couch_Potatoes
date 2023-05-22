import React, { FC } from 'react';
import { Grid, HStack } from '@chakra-ui/react';
import Movie from '../../models/movie';
import FavoriteMovieCard from './FavoriteMovieCard';

export interface FavoriteMovieProps {
    movies: Movie[];
}

const FavoriteMovieRow: FC<FavoriteMovieProps> = ({ movies }) => {
    return (
        <HStack gap={2}>
            {movies.map((movie) => (
                <FavoriteMovieCard movie={movie} />
            ))}
        </HStack>
    );
};

export default FavoriteMovieRow;
