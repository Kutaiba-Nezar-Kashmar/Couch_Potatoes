import React, { FC } from 'react';
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

    const getTotalVotes = (reviewList: Review[]) => {
        return reviewList.flatMap((review) => review.votes).length;
    };

    const getAllUpvotes = (reviewList: Review[]) => {
        return reviewList
            .flatMap((review) => review.votes)
            .filter((review) => review.direction == 'Up').length;
    };

    const getAverageUpvotes = () => {
        return getAllUpvotes(reviews) / getTotalVotes(reviews);
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
                    {sliceNumber(
                        reviews.reduce((a, b) => a + b.rating, 0) /
                            reviews.length,
                        1
                    )}
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
                    {sliceNumber(
                        isNaN(getAverageUpvotes()) ? 0 : getAverageUpvotes(),
                        1
                    )}
                </Heading>
            </Flex>
        </Flex>
    );
};

export default ReviewsStats;
