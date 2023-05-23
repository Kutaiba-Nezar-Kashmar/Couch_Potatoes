import React, { FC, useEffect, useState } from 'react';
import { Review } from '../../models/review/review';
import { Box, Divider, Flex, Heading, VStack } from '@chakra-ui/react';
import { Theme } from '../../models/theme';
import { getTextColor } from '../../util/themeutil';
import ReviewComponent from './ReviewComponent';
import ReviewsStats from './ReviewsStats';
import { subscribeRemoveReviewEmitter } from '../../services/event-emitters/review-emitter';
import { EventListener } from '../../services/event-emitters/event-listener';

export interface ReviewListProps {
    reviewsProp: Review[];
    theme: Theme;
    title: string;
}

const ReviewList: FC<ReviewListProps> = ({ reviewsProp, theme, title }) => {
    const [reviews, setReviews] = useState<Review[]>(reviewsProp);

    const removeReviewListener: EventListener = {
        onEvent(data) {
            reviewsProp = reviewsProp.filter(
                (review) => review.reviewId !== data!
            );
            setReviews(reviews.filter((review) => review.reviewId !== data!));
        },
    };

    useEffect(() => {
        subscribeRemoveReviewEmitter(removeReviewListener);
        setReviews(reviewsProp);
    }, [reviewsProp]);

    return (
        <>
            <Heading
                as="h3"
                size={{ base: 'lg', sm: 'md', md: 'lg' }}
                textAlign="start"
                textColor={getTextColor(theme)}
                color={getTextColor(theme)}
            >
                {title}
            </Heading>
            <Box marginBottom="1rem" marginTop="3rem">
                <ReviewsStats theme={theme} reviews={reviews} />
            </Box>
            <Flex direction="column" gap={6}>
                <Divider />
                {reviews.map((review, index) => (
                    <>
                        <ReviewComponent theme={theme} review={review} />
                        {index !== reviews.length - 1 && <Divider />}
                    </>
                ))}
            </Flex>
        </>
    );
};

export default ReviewList;
