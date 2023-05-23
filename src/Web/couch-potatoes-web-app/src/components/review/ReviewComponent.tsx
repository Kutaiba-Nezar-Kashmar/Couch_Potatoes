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
    useToast,
} from '@chakra-ui/react';
import ReviewUserInfo from './ReviewUserInfo';
import StarRatingComponent from 'react-star-rating-component';
import { useFetchMovieDetails } from '../../services/movie-details';
import Movie from '../../models/movie';
import {
    ArrowDownIcon,
    ArrowUpIcon,
    CheckIcon,
    EditIcon,
} from '@chakra-ui/icons';
import User from '../../models/user';
import {
    getAuthenticatedUser,
    useGetAuthenticatedUser,
} from '../../services/user';
import { Vote } from '../../models/review/vote';
import { useNavigate } from 'react-router';
import { useQueryClient } from 'react-query';
import {
    ReviewCacheKeys,
    updateReview,
    voteReview,
} from '../../services/review';
import { slowClone } from '../../util/clone';

export interface ReviewComponentProps {
    review: Review;
}

const ReviewComponent: FC<ReviewComponentProps> = ({ review: reviewProp }) => {
    const [movie, setMovie] = useState<Movie>(reviewProp.movie);
    const [review, setReview] = useState<Review>(reviewProp);
    const [isEditing, setIsEditing] = useState(false);

    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const toast = useToast();

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
        console.log(review);
    }, [isLoadingUser, review]);

    function getUpvoteCount() {
        const upvotes = review.votes.filter((r) => r.direction == 'Up').length;
        if (isNaN(upvotes)) {
            return 0;
        }

        return upvotes;
    }

    function getDownvoteCount() {
        const upvotes = review.votes.filter(
            (r) => r.direction == 'Down'
        ).length;
        if (isNaN(upvotes)) {
            return 0;
        }

        return upvotes;
    }

    function userOwnsReview(user: User | null, review: Review) {
        if (!user) {
            return false;
        }

        return review.user.id === user.id;
    }

    async function vote(direction: string) {
        if (!authenticatedUser) {
            navigate('/login');
        }

        try {
            toast({
                status: 'info',
                title: 'Voting',
                description: `${direction}voting the review...`,
                duration: 120000,
                isClosable: true,
            });
            const vote = await voteReview(
                authenticatedUser!.id,
                movie.id,
                review.reviewId,
                direction
            );
            console.log(vote);

            const existingVote = review.votes.find(
                (v) => v.userId === authenticatedUser?.id
            );

            if (!vote) {
                // User did already vote and has not deleted his vote.
                if (!existingVote) {
                    queryClient.invalidateQueries([
                        ReviewCacheKeys.REVIEWS_FOR_MOVIE + '' + movie.id,
                        ReviewCacheKeys.REVIEWS_FOR_USER +
                            authenticatedUser!.id,
                    ]);
                    setReview(slowClone(review));
                    return;
                }
                review.votes = review.votes.filter(
                    (v) => v.id !== existingVote.id
                );
                queryClient.invalidateQueries([
                    ReviewCacheKeys.REVIEWS_FOR_MOVIE + '' + movie.id,
                    ReviewCacheKeys.REVIEWS_FOR_USER + authenticatedUser!.id,
                ]);
                setReview(slowClone(review));
                return;
            }

            // User has not voted and did not have an existing vote
            if (!existingVote) {
                review.votes.push(vote);
                queryClient.invalidateQueries([
                    ReviewCacheKeys.REVIEWS_FOR_MOVIE + '' + movie.id,
                    ReviewCacheKeys.REVIEWS_FOR_USER + authenticatedUser!.id,
                ]);
                setReview(slowClone(review));
                return;
            }

            // User has  existing vote that needs to be updated
            review.votes = review.votes.filter((v) => v.id !== existingVote.id);
            review.votes.push(vote);
            queryClient.invalidateQueries([
                ReviewCacheKeys.REVIEWS_FOR_MOVIE + '' + movie.id,
                ReviewCacheKeys.REVIEWS_FOR_USER + authenticatedUser!.id,
            ]);
            setReview(slowClone(review));
        } catch (err) {
            console.error(err);
            toast({
                status: 'error',
                title: 'Error',
                description: 'Failed to vote review',
                duration: 4000,
                isClosable: true,
            });
        }
    }

    function useHasVotedInDirection(direction: string) {
        if (!authenticatedUser) {
            return false;
        }

        const existingVote = review.votes.find(
            (vote) => vote.userId === authenticatedUser.id
        );
        if (!existingVote) {
            return false;
        }

        return existingVote.direction === direction;
    }

    async function updateUsersReview() {
        if (!authenticatedUser) {
            return;
        }

        try {
            const reviewDto = await updateReview(
                authenticatedUser!.id,
                movie.id,
                review.reviewId,
                review.rating,
                review.reviewText
            );

            review.rating = reviewDto.rating;
            review.reviewText = reviewDto.reviewText;
            review.lastUpdatedDate = reviewDto.lastUpdatedDate;

            setReview(slowClone(review));
            setIsEditing(false);
            toast({
                status: 'success',
                title: 'Review Updated!',
                duration: 4000,
                isClosable: true,
            });
        } catch (err) {
            console.error(err);
            toast({
                status: 'error',
                title: 'Error',
                description: 'Failed to update review',
                duration: 4000,
                isClosable: true,
            });
        }
    }

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
                <Flex direction="row" justify="space-between" width="100%">
                    <StarRatingComponent
                        value={review.rating}
                        name="reviewScore"
                        starCount={10}
                        editing={isEditing}
                        emptyStarColor="white"
                        onStarClick={(nextValue, prevValue, name) => {
                            if (!isEditing) {
                                return;
                            }
                            console.log(nextValue);
                            review.rating = nextValue;
                            setReview(slowClone(review));
                        }}
                    />
                    <Text textColor="#D8DEE9">
                        {new Date(review.creationDate).toLocaleDateString()}
                    </Text>
                </Flex>
                <Text as="em" color="#ECEFF4">
                    Last edited:{' '}
                    {new Date(review.lastUpdatedDate).toLocaleDateString()}
                </Text>

                <Textarea
                    isReadOnly={!isEditing}
                    onChange={(e) => {
                        review.reviewText = e.target.value;
                        setReview(slowClone(review));
                    }}
                    textColor="#ECEFF4"
                    value={review.reviewText}
                />

                <HStack justify="end" marginTop="1rem">
                    {userOwnsReview(authenticatedUser, review) &&
                        (!isEditing ? (
                            <Tooltip label="Edit review">
                                <>
                                    <EditIcon
                                        color="white"
                                        boxSize={6}
                                        _hover={{ color: 'black' }}
                                        transition="all 0.2s ease-in-out"
                                        cursor="pointer"
                                        onClick={() => setIsEditing(true)}
                                    />
                                </>
                            </Tooltip>
                        ) : (
                            <Tooltip label="Finish">
                                <>
                                    <CheckIcon
                                        color="white"
                                        boxSize={6}
                                        _hover={{ color: 'black' }}
                                        transition="all 0.2s ease-in-out"
                                        cursor="pointer"
                                        onClick={() => {
                                            updateUsersReview();
                                        }}
                                    />
                                </>
                            </Tooltip>
                        ))}
                    <Tooltip label="Upvote">
                        <>
                            <ArrowUpIcon
                                color={
                                    useHasVotedInDirection('Up')
                                        ? 'green'
                                        : 'white'
                                }
                                boxSize={6}
                                _hover={{ color: 'green' }}
                                transition="all 0.2s ease-in-out"
                                cursor="pointer"
                                onClick={async () => {
                                    await vote('Up');
                                }}
                            />
                            <Text textColor="#ECEFF4">{getUpvoteCount()}</Text>
                        </>
                    </Tooltip>
                    <Tooltip label="Downvote">
                        <>
                            <ArrowDownIcon
                                boxSize={6}
                                color={
                                    useHasVotedInDirection('Down')
                                        ? 'red'
                                        : 'white'
                                }
                                _hover={{ color: 'red' }}
                                transition="all 0.2s ease-in-out"
                                cursor="pointer"
                                onClick={async () => {
                                    await vote('Down');
                                }}
                            />
                            <Text textColor="#ECEFF4">
                                {getDownvoteCount()}
                            </Text>
                        </>
                    </Tooltip>
                </HStack>
            </Flex>
        </Flex>
    );
};

export default ReviewComponent;
