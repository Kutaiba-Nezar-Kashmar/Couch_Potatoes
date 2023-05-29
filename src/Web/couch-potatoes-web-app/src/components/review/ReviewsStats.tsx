import React, { FC, useEffect, useState } from 'react';
import { Review } from '../../models/review/review';
import {
    Box,
    Divider,
    Flex,
    HStack,
    Heading,
    Text,
    VStack,
} from '@chakra-ui/react';
import { sliceNumber } from '../../util/numberutil';
import { Theme } from '../../models/theme';
import { getTextColor } from '../../util/themeutil';

export interface ReviewsStatsProp {
    reviews: Review[];
    theme: Theme;
}

const ReviewsStats: FC<ReviewsStatsProp> = ({ theme, reviews }) => {
    const getListLengthString = (xs: any[]) => {
        if (xs.length < 1000) {
            return reviews.length + '';
        }

        return xs.length / 1000 + 'k';
    };

    const getAverageUpvotes = () => {
        const total = reviews.flatMap((review) => review.votes)?.length ?? 0;
        const upvotes =
            reviews
                .flatMap((review) => review.votes)
                .filter((review) => review.direction == 'Up')?.length ?? 0;

        const averageUpvotes = upvotes / total;

        if (isNaN(averageUpvotes)) {
            return 0;
        }

        return averageUpvotes;
    };

    const getAverageRating = () => {
        const avgRating = Number(
            sliceNumber(
                reviews.reduce((a, b) => a + b.rating, 0) / reviews.length,
                1
            )
        );

        if (isNaN(avgRating)) {
            return 0;
        }

        return avgRating;
    };

    return (
        <Flex direction="row">
            <Flex direction="column" justify="start" alignItems="center">
                <Heading
                    as="h6"
                    size="md"
                    textColor={getTextColor(theme)}
                    textAlign="center"
                >
                    Total Reviews
                </Heading>
                <Heading
                    textColor={getTextColor(theme)}
                    size="xl"
                    textAlign="center"
                >
                    {getListLengthString(reviews)}
                </Heading>
            </Flex>
            <Box height="100px" marginX="6rem">
                <Divider orientation="vertical" />
            </Box>
            <Flex direction="column" justify="start" alignItems="center">
                <Heading
                    as="h6"
                    size="md"
                    textColor={getTextColor(theme)}
                    textAlign="center"
                >
                    Average rating
                </Heading>
                <Heading
                    textColor={getTextColor(theme)}
                    size="xl"
                    textAlign="center"
                >
                    {sliceNumber(getAverageRating(), 1)}
                </Heading>
            </Flex>
            <Box height="100px" marginX="6rem">
                <Divider orientation="vertical" />
            </Box>
            <Flex direction="column" justify="start" alignItems="center">
                <Heading
                    as="h6"
                    size="md"
                    textColor={getTextColor(theme)}
                    textAlign="center"
                >
                    Average Upvotes
                </Heading>
                <Heading
                    textColor={getTextColor(theme)}
                    size="xl"
                    textAlign="center"
                >
                    {sliceNumber(getAverageUpvotes(), 1)}
                </Heading>
            </Flex>
        </Flex>
    );
};

export default ReviewsStats;
