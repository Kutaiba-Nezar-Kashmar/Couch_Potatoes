import React, { FC, useEffect, useState } from 'react';
import {
    Heading,
    Grid,
    VStack,
    Flex,
    Text,
    ResponsiveValue,
    useBreakpointValue,
    Tooltip,
} from '@chakra-ui/react';
import Movie from '../../models/movie';
import { Theme } from '../../models/theme';
import { groupElements } from '../../util/listutil';
import FavoriteMovieRow from './FavoriteMovieRow';
import { CheckIcon, EditIcon } from '@chakra-ui/icons';
import { getTextColor } from '../../util/themeutil';
import { useWindowSize } from '../../hooks/useWindowSize';

export interface FavoriteMovieDirectory {
    theme: Theme;
    movies: Movie[];
}

const FavoriteMoviesDirectory: FC<FavoriteMovieDirectory> = ({
    theme,
    movies,
}) => {
    const [moviesPerRow, setMoviesPerRow] = useState<number>(5);
    const [editing, setEditing] = useState(false);

    let moviesPerRowToDisplay = useBreakpointValue({
        base: 2,
        md: 3,
        lg: 5,
        xl: 7,
    });

    useEffect(() => {
        setMoviesPerRow(moviesPerRowToDisplay ?? 5);
    }, [movies]);

    return (
        <>
            <Flex direction="row" justify="start" width="100%">
                <Heading
                    as="h3"
                    size={{ base: 'lg', sm: 'md', md: 'lg' }}
                    textAlign="start"
                    textColor={getTextColor(theme)}
                >
                    Favorites{' '}
                    {!editing ? (
                        <Tooltip label="Edit favorite movies list">
                            <EditIcon
                                cursor="pointer"
                                onClick={() => setEditing(!editing)}
                            />
                        </Tooltip>
                    ) : (
                        <CheckIcon
                            cursor="pointer"
                            onClick={() => setEditing(!editing)}
                        />
                    )}
                </Heading>
            </Flex>
            <Flex direction="column" justify="start" marginTop="0.5rem" gap={5}>
                {groupElements(movies, moviesPerRow).map((row) => (
                    <FavoriteMovieRow editing={editing} movies={row} />
                ))}
            </Flex>
        </>
    );
};

export default FavoriteMoviesDirectory;
