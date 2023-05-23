import React, { FC, useEffect, useState } from 'react';
import { Review } from '../../models/review/review';
import {
    Flex,
    HStack,
    Heading,
    Image,
    Text,
    Textarea,
    Tooltip,
    VStack,
} from '@chakra-ui/react';
import ReviewUserInfo from './ReviewUserInfo';
import StarRatingComponent from 'react-star-rating-component';
import { useFetchMovieDetails } from '../../services/movie-details';
import Movie from '../../models/movie';
import { ArrowDownIcon, ArrowUpIcon, EditIcon } from '@chakra-ui/icons';
import User from '../../models/user';
import {
    getAuthenticatedUser,
    useGetAuthenticatedUser,
} from '../../services/user';
import { Vote } from '../../models/review/vote';

export interface ReviewComponentProps {
    review: Review;
}

const ReviewComponent: FC<ReviewComponentProps> = ({ review }) => {
    const [movie, setMovie] = useState<Movie>(review.movie);
    const [isEditing, setIsEditing] = useState(false);

    const [authenticatedUser, setAuthenticatedUser] = useState<User | null>(
        null
    );

    const {
        isLoading: isLoadingUser,
        isError: isErrorUser,
        data: userData,
        error: userError,
    } = useGetAuthenticatedUser();

    useEffect(() => {
        if (!isLoadingUser) {
            setAuthenticatedUser(userData ?? null);
        }
    }, [isLoadingUser]);

    function getUpvoteCount(r: Review) {
        const upvotes = r.votes.filter((r) => r.direction == 'Up').length;
        if (isNaN(upvotes)) {
            return 0;
        }

        return upvotes;
    }

    function getDownvoteCount(r: Review) {
        const upvotes = r.votes.filter((r) => r.direction == 'Down').length;
        if (isNaN(upvotes)) {
            return 0;
        }

        return upvotes;
    }

    function userOwnsReview(user: User, review: Review) {
        return review.user.id === user.id;
    }

    const upvote = (user: User, movieId: number, reviewId: string) => {};

    return (
        <Flex
            direction="row"
            gap={10}
            minHeight={{ base: '150px', md: '250px' }}
        >
            <ReviewUserInfo user={review.user} />
            <Flex direction="column">
                <Text color="#ECEFF4" textTransform="uppercase">
                    {movie.title} - <em>{movie.tagLine}</em>
                </Text>
                <HStack>
                    <StarRatingComponent
                        value={review.rating}
                        name="reviewScore"
                        starCount={10}
                    />
                    <Text textColor="#D8DEE9">
                        {new Date(review.creationDate).toLocaleDateString()}
                    </Text>
                </HStack>
                <Text as="em" color="#ECEFF4">
                    Last edited:{' '}
                    {new Date(review.lastUpdatedDate).toLocaleDateString()}
                </Text>

                <Textarea
                    isReadOnly={!isEditing}
                    textColor="#ECEFF4"
                    value={review.reviewText}
                />
                {authenticatedUser && (
                    <HStack justify="end" marginTop="1rem">
                        {userOwnsReview(authenticatedUser, review) && (
                            <Tooltip label="Edit review">
                                <>
                                    <EditIcon
                                        color="white"
                                        boxSize={6}
                                        _hover={{ color: 'black' }}
                                        transition="all 0.2s ease-in-out"
                                        cursor="pointer"
                                    />
                                </>
                            </Tooltip>
                        )}
                        <Tooltip label="Upvote">
                            <>
                                <ArrowUpIcon
                                    color="white"
                                    boxSize={6}
                                    _hover={{ color: 'green' }}
                                    transition="all 0.2s ease-in-out"
                                    cursor="pointer"
                                />
                                <Text textColor="#ECEFF4">
                                    {getUpvoteCount(review)}
                                </Text>
                            </>
                        </Tooltip>
                        <Tooltip label="Downvote">
                            <>
                                <ArrowDownIcon
                                    boxSize={6}
                                    color="white"
                                    _hover={{ color: 'red' }}
                                    transition="all 0.2s ease-in-out"
                                    cursor="pointer"
                                />
                                <Text textColor="#ECEFF4">
                                    {getDownvoteCount(review)}
                                </Text>
                            </>
                        </Tooltip>
                    </HStack>
                )}
            </Flex>
        </Flex>
    );
};

export default ReviewComponent;
