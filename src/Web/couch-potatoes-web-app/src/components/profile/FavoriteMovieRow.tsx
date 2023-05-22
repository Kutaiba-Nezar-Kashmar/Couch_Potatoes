import React, { FC, useState } from 'react';
import { Grid, HStack } from '@chakra-ui/react';
import Movie from '../../models/movie';
import FavoriteMovieCard from './FavoriteMovieCard';

export interface FavoriteMovieProps {
    movies: Movie[];
    editing: boolean;
}

const FavoriteMovieRow: FC<FavoriteMovieProps> = ({ movies, editing }) => {
    return (
        <HStack gap={2}>
            {movies.map((movie) => (
                <FavoriteMovieCard editing={editing} movie={movie} />
            ))}
        </HStack>
    );
};

export default FavoriteMovieRow;
