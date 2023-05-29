import React, { FC, useEffect, useState } from 'react';
import BasePage from '../components/BasePage';
import {
    Box,
    Flex,
    Grid,
    GridItem,
    HStack,
    Spinner,
    Text,
    VStack,
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
    useGetAuthenticatedUser,
    useGetUserById,
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
import { Review } from '../models/review/review';
import ReviewList from '../components/review/ReviewList';
import { useGetReviewsForUser } from '../services/review';
import { getDarkGrayBackground } from '../util/themeutil';

const ProfilePage: FC = () => {
    const [user, setUser] = useState<User | null>(null);
    const [favoriteMovies, setFavoriteMovies] = useState<Movie[]>([]);
    const [reviews, setReviews] = useState<Review[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const { userId } = useParams();

    const navigate = useNavigate();
    const toast = useToast();
    const queryClient = useQueryClient();

    const {
        isLoading: isLoadingFavorites,
        isError: isErrorFavorites,
        data,
        error: favoritesError,
    } = useFetchMovies(user ? user.favoriteMovies! : [], () => {
        return (
            user !== null &&
            user.favoriteMovies !== null &&
            user.favoriteMovies !== undefined &&
            user.favoriteMovies.length > 0
        );
    });

    const {
        isLoading: isLoadingAuthUser,
        isError: isErrorAuthUser,
        data: authUserData,
        error: authUserError,
    } = useGetAuthenticatedUser();

    const {
        isLoading: isLoadingUser,
        isError: isErrorUser,
        data: userData,
        error: userError,
    } = useGetUserById(userId);

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

        queryClient.invalidateQueries([
            UserCacheKeys.GET_USER_WITH_ID + user.id,
            UserCacheKeys.AUTHENTICATED_USER,
        ]);

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

    const {
        isLoading: isLoadingReviews,
        isError: isErrorReviews,
        data: reviewsData,
        error: reviewsError,
    } = useGetReviewsForUser(user?.id);

    useEffect(() => {
        subscribeRemoveFavoriteMovieEventListener(removeFavoriteListener);
        if (!isLoadingAuthUser && !isLoadingUser) {
            if (userData) {
                setUser(userData);
            } else {
                setUser(authUserData ?? null);
            }
        }
        if (!isLoadingFavorites) {
            setFavoriteMovies(data ?? []);
        }

        if (!isLoadingReviews) {
            console.log(reviewsData);
            setReviews(reviewsData ?? []);
        }

        return () => {
            unsubscribeRemoveFavoriteMovieEvent(removeFavoriteListener);
            if (user) {
                queryClient.invalidateQueries([
                    UserCacheKeys.GET_USER_WITH_ID + user.id,
                    UserCacheKeys.AUTHENTICATED_USER,
                ]);
            }
        };
    }, [
        isLoadingFavorites,
        isLoadingReviews,
        isLoadingAuthUser,
        isLoadingUser,
    ]);

    if (isLoadingAuthUser) {
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
        <BackgroundImageFull imageUri={getDarkGrayBackground()}>
            <BasePage>
                <Flex flexDir="row" width="100%" height="100%">
                    <Text> User does not exist</Text>
                </Flex>
            </BasePage>
        </BackgroundImageFull>;
    }

    return (
        <BackgroundImageFull imageUri={getDarkGrayBackground()}>
            <BasePage>
                <Flex direction="row" minHeight="100vh" width="100%" gap={16}>
                    <Box>
                        <ProfileInfo theme={Theme.DARK} user={user} />
                    </Box>

                    <Flex direction="column">
                        <Box>
                            <FavoriteMovieDirectory
                                movies={favoriteMovies}
                                theme={Theme.DARK}
                            />
                        </Box>
                        <Box marginTop="2rem">
                            {isLoadingReviews ? (
                                <Spinner
                                    thickness="4px"
                                    speed="0.65s"
                                    emptyColor="gray.200"
                                    color="blue.500"
                                    size="xl"
                                />
                            ) : (
                                <ReviewList
                                    theme={Theme.DARK}
                                    reviewsProp={reviews}
                                    title="User's reviews"
                                />
                            )}
                        </Box>
                    </Flex>
                </Flex>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default ProfilePage;
