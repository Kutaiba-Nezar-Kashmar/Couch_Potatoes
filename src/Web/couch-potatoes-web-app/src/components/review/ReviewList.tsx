import React, { FC, useState } from 'react';
import { Review } from '../../models/review/review';
import { Box, Divider, Flex, Heading, VStack } from '@chakra-ui/react';
import { Theme } from '../../models/theme';
import { getTextColor } from '../../util/themeutil';
import ReviewComponent from './ReviewComponent';
import ReviewsStats from './ReviewsStats';

export interface ReviewListProps {
    reviews: Review[];
    theme: Theme;
    title: string;
}

const ReviewList: FC<ReviewListProps> = ({ reviews, theme, title }) => {
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
                <ReviewsStats reviews={reviews} />
            </Box>
            <Flex direction="column" gap={6}>
                <Divider />
                {reviews.map((review, index) => (
                    <>
                        <ReviewComponent review={review} />
                        {index !== reviews.length - 1 && <Divider />}
                    </>
                ))}
            </Flex>
        </>
    );
};

export default ReviewList;
