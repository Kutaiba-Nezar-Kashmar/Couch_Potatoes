import {
    Box,
    Flex,
    Image,
    Stack,
    StackDivider,
    Text,
    Tooltip,
    useToast,
} from '@chakra-ui/react';
import React, {FC, useEffect, useState} from 'react';
import starIcon from '../../assets/iconstar.png';
import {sliceNumber} from '../../util/numberutil';
import {AddIcon} from '@chakra-ui/icons';
import Movie from '../../models/movie';
import User from '../../models/user';
import {useMutation, useQueryClient} from 'react-query';
import {useNavigate} from 'react-router-dom';
import {UserCacheKeys, addFavoriteMovie} from '../../services/user';

export interface MovieDetailsHeaderComponentProps {
    movie: Movie | null;
    themeColor: string;
    user: User | null;
}

export const MovieDetailsHeaderInformationbox: FC<
    MovieDetailsHeaderComponentProps
> = ({movie, user}) => {
    const [isFavorite, setIsFavorite] = useState(false);

    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const toast = useToast();

    const addMovieToFavorite = async (movieId: number) => {
        if (!user) {
            navigate('/Couch_Potatoes/login');
        }

        const movieWasAdded = await addFavoriteMovie(user!.id, movieId);

        if (!movieWasAdded) {
            toast({
                title: 'Error!',
                description: 'Failed to add movie to favorite',
                status: 'error',
                duration: 4000,
                isClosable: true,
            });
            return;
        }
        toast({
            title: 'Sucess!',
            description: 'Movie has been added to your favorites',
            status: 'success',
            duration: 4000,
            isClosable: true,
        });

        queryClient.invalidateQueries([
            UserCacheKeys.GET_USER_WITH_ID + user!.id,
            UserCacheKeys.AUTHENTICATED_USER,
        ]);
        setIsFavorite(true);
    };

    useEffect(() => {
        if (!movie) {
            return;
        }

        if (!user) {
            setIsFavorite(false);
        } else {
            setIsFavorite(
                user.favoriteMovies?.find((movieId) => movieId == movie.id) !=
                undefined
                    ? true
                    : false
            );
        }
        console.log(user);
    }, [movie]);

    return (
        <>
            <Stack direction="row" justify="space-between">
                <Stack
                    direction="column"
                    divider={<StackDivider borderColor="gray.200"/>}
                >
                    <Stack direction="row">
                        <Box>
                            <Text color={'white'} fontSize="3xl">
                                {movie?.title ? (movie?.title) : ("N/A")}
                            </Text>
                        </Box>

                        <Box>
                            <Text color={'gray'} fontSize="3xl">
                                {movie?.releaseDate ? ("("+movie?.releaseDate.slice(0, 4)+")") : ("N/A")}

                            </Text>
                        </Box>

                        {!isFavorite && user && (
                            <Flex
                                direction="row"
                                justify="center"
                                alignItems="center"
                            >
                                <Tooltip label="Add to favorites">
                                    <AddIcon
                                        cursor="pointer"
                                        transition="all 0.3s ease-in-out"
                                        color="white"
                                        onClick={() =>
                                            movie &&
                                            addMovieToFavorite(movie.id)
                                        }
                                        _hover={{color: 'yellow'}}
                                    />
                                </Tooltip>
                            </Flex>
                        )}
                    </Stack>

                    <Box>
                        <Text fontStyle="italic" color={'white'} fontSize="2l">
                            {movie?.tagLine ? (movie?.tagLine) : ("N/A")}
                        </Text>
                    </Box>
                </Stack>

                <Box>
                    <Stack direction="row">
                        <Image src={starIcon} maxHeight={8}/>
                        <Text color={'white'} fontSize="2xl">
                            {sliceNumber(Number(movie?.tmdbScore), 1)}/10
                            {/* YEAR AND RUNTIME */}
                        </Text>
                    </Stack>

                    <Text color={'white'} fontSize="1xl" align="end">
                        Vote#: {movie?.tmdbVoteCount ? (movie?.tmdbVoteCount) : ("N/A")}
                    </Text>
                </Box>
            </Stack>
        </>
    );
};
