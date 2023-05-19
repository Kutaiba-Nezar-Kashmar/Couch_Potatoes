import React, { FC } from 'react';
import { Heading } from '@chakra-ui/react';
import Movie from '../../models/movie';

export enum ColorScheme {
    DARK,
    LIGHT,
}

export interface FavoriteMovieDirectory {
    colorScheme: ColorScheme;
    movies: Movie[];
}

const FavoriteMoviesDirectory: FC<FavoriteMovieDirectory> = ({
    colorScheme,
    movies,
}) => {
    const getTextColor = () => {
        return colorScheme == ColorScheme.DARK ? 'white' : 'black';
    };

    return (
        <>
            <Heading as="h3" textColor={getTextColor()}>
                Favorites
            </Heading>
        </>
    );
};

export default FavoriteMoviesDirectory;
