import React, { FC, useEffect, useState } from 'react';
import BasePage from '../components/BasePage';
import {
    Flex,
    Grid,
    GridItem,
    Spinner,
    Text,
    useToast,
} from '@chakra-ui/react';
import ProfileInfo from '../components/profile/ProfileInfo';
import User from '../models/user';
import { auth } from '../firebase';
import { useNavigate } from 'react-router-dom';
import {
    UserCacheKeys,
    deleteFavoriteMovieForUser,
    getAuthenticatedUser,
    getUserById,
} from '../services/user';
import BackgroundImageFull from '../components/BackgroundImageFull';
import { navBarHeightInRem } from '../components/settings/page-settings';
import FavoriteMovieDirectory from '../components/profile/FavoriteMovieDirectory';
import { useParams } from 'react-router-dom';
import Movie from '../models/movie';
import { useFetchMovies } from '../services/movie-details';
import { Theme } from '../models/theme';
import {
    subscribeRemoveFavoriteMovieEventListener,
    removeFavoriteMovieEmitter,
    unsubscribeRemoveFavoriteMovieEvent,
} from '../services/event-emitters/favorite-movie-emitter';
import { useQueryClient } from 'react-query';
import { debounce, debounceTime, interval } from 'rxjs';
import { EventListener } from '../services/event-emitters/event-listener';

const ProfilePage: FC = () => {
    const [user, setUser] = useState<User | null>(null);
    const [favoriteMovies, setFavoriteMovies] = useState<Movie[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const { userId } = useParams();

    const navigate = useNavigate();
    const toast = useToast();
    const queryClient = useQueryClient();

    const {
        isLoading: isLoadingFavorites,
        isError,
        data,
        error,
    } = useFetchMovies(user ? user.favoriteMovies! : [], () => {
        return (
            user !== null &&
            user.favoriteMovies !== null &&
            user.favoriteMovies !== undefined &&
            user.favoriteMovies.length > 0
        );
    });

    const initProfile = async () => {
        setLoading(true);

        if (!userId) {
            const currentUser = await getAuthenticatedUser();
            if (currentUser) {
                setUser(currentUser);
                setLoading(false);
                return;
            } else {
                navigate('/login');
                return;
            }
        }

        const userToDisplay = await getUserById(userId);
        if (userToDisplay) {
            setUser(userToDisplay);
            setLoading(false);
        } else {
            setUser(null);
            setLoading(false);
        }
    };

    const removeMovieFromFavorite = async (movieId: number): Promise<any> => {
        if (!user) {
            return;
        }
        const removed = await deleteFavoriteMovieForUser(user?.id, movieId);
        if (!removed) {
            toast({
                title: 'Server error',
                description: 'Failed to remove movie from favorites.',
                status: 'error',
                duration: 4000,
                isClosable: true,
            });

            return;
        }

        setFavoriteMovies(
            favoriteMovies.filter((movie) => movie.id !== movieId)
        );

        queryClient.invalidateQueries(UserCacheKeys.GET_USER_WITH_ID + user.id);

        toast({
            title: 'Sucess',
            description: `Removed movie from favorites`,
            status: 'success',
            duration: 4000,
            isClosable: true,
        });
    };

    let removeFavoriteListener: EventListener = {
        onEvent(data) {
            removeMovieFromFavorite(data);
        },
    };

    useEffect(() => {
        subscribeRemoveFavoriteMovieEventListener(removeFavoriteListener);
        initProfile();
        setFavoriteMovies(data ?? []);
        return () => {
            unsubscribeRemoveFavoriteMovieEvent(removeFavoriteListener);
        };
    }, [isLoadingFavorites, favoriteMovies]);

    if (loading) {
        return (
            <Flex
                width="100%"
                height="100%"
                justifyContent="center"
                alignItems="center"
            >
                <Spinner
                    thickness="4px"
                    speed="0.65s"
                    emptyColor="gray.200"
                    color="blue.500"
                    size="xl"
                />
            </Flex>
        );
    }

    if (!user) {
        <BackgroundImageFull
            imageUri={`${process.env['PUBLIC_URL']}/gradient-bg.jpg`}
        >
            <BasePage>
                <Flex flexDir="row" width="100%" height="100%">
                    <Text> User does not exist</Text>
                </Flex>
            </BasePage>
        </BackgroundImageFull>;
    }

    return (
        <BackgroundImageFull
            imageUri={`${process.env['PUBLIC_URL']}/gradient-bg.jpg`}
        >
            <BasePage>
                <Grid
                    width="100%"
                    height="100%"
                    templateColumns="repeat(100, 1fr)"
                >
                    <GridItem colSpan={20}>
                        <ProfileInfo theme={Theme.DARK} user={user} />
                    </GridItem>
                    <GridItem colSpan={80}>
                        <FavoriteMovieDirectory
                            movies={favoriteMovies}
                            theme={Theme.DARK}
                        />
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default ProfilePage;
