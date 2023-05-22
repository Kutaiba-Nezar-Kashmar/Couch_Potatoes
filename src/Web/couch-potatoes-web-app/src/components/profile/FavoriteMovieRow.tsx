import React, { FC, useState } from 'react';
import { Grid, HStack } from '@chakra-ui/react';
import Movie from '../../models/movie';
import FavoriteMovieCard from './FavoriteMovieCard';

export interface FavoriteMovieProps {
    movies: Movie[];
    editing: boolean;
}

const FavoriteMovieRow: FC<FavoriteMovieProps> = ({ movies, editing }) => {
    const [isEditing, setIsEditing] = useState<boolean>(editing);

    return (
        <HStack gap={2}>
            {movies.map((movie) => (
                <FavoriteMovieCard editing={isEditing} movie={movie} />
            ))}
        </HStack>
    );
};

export default FavoriteMovieRow;
