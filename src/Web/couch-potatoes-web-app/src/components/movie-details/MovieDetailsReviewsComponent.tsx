import {
    Box,
    Button,
    Card,
    CardBody,
    CardHeader,
    Flex,
    FormControl,
    FormLabel,
    HStack,
    Heading,
    Input,
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
    Stack,
    StackDivider,
    Text,
    Tooltip,
    VStack,
    useDisclosure,
    useToast,
} from '@chakra-ui/react';
import React, { FC, useEffect, useState } from 'react';
import MovieCredits from '../../models/movie_credits';
import ReviewList from '../review/ReviewList';
import { Theme } from '../../models/theme';
import { Review } from '../../models/review/review';
import { createReview, useGetReviewsForMovie } from '../../services/review';
import { PlusSquareIcon } from '@chakra-ui/icons';
import StarRatingComponent from 'react-star-rating-component';
import User from '../../models/user';
import { Navigate, useNavigate } from 'react-router-dom';

export interface MovieDetailsReviewsComponentProps {
    reviews: Review[];
    movieId: number;
    setReviews: React.Dispatch<React.SetStateAction<Review[]>>;
    authenticatedUser: User | null;
}
export const MovieDetailsReviewsComponent: FC<
    MovieDetailsReviewsComponentProps
> = ({ reviews, setReviews, movieId, authenticatedUser }) => {
    const { isOpen, onOpen, onClose } = useDisclosure();

    const [rating, setRating] = useState(0);
    const [reviewText, setReviewText] = useState('');

    const initialRef = React.useRef(null);
    const finalRef = React.useRef(null);
    const toast = useToast();
    const navigate = useNavigate();

    async function create() {
        try {
            if (!authenticatedUser) {
                navigate('/Couch_Potatoes/login');
            }

            const createdReview = await createReview(
                movieId,
                authenticatedUser!.id,
                rating,
                reviewText
            );

            setReviews([...reviews, createdReview]);
            onClose();
        } catch (err) {
            console.error(err);
            toast({
                status: 'error',
                title: 'Error',
                description: 'Failed to create review',
                duration: 4000,
                isClosable: true,
            });
        }
    }

    return (
        <>
            <Card>
                <CardHeader>
                    <HStack>
                        <Heading size="md">Reviews</Heading>
                        <Tooltip label="Add new review">
                            <PlusSquareIcon
                                cursor="pointer"
                                transition="0.3s ease-in-out"
                                _hover={{ color: 'green' }}
                                onClick={() => {
                                    if (!authenticatedUser) {
                                        navigate('/Couch_Potatoes/login');
                                    }
                                    onOpen();
                                }}
                                boxSize={6}
                            />
                        </Tooltip>
                    </HStack>
                </CardHeader>
                <CardBody width="100%">
                    <Flex width="100%" direction="row" justify="center">
                        <Stack divider={<StackDivider />} spacing="4">
                            <ReviewList
                                theme={Theme.LIGHT}
                                title=""
                                reviewsProp={reviews}
                            />
                        </Stack>
                    </Flex>
                </CardBody>
            </Card>

            <Modal
                initialFocusRef={initialRef}
                finalFocusRef={finalRef}
                isOpen={isOpen}
                onClose={onClose}
            >
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Create new review</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody pb={6}>
                        <FormControl>
                            <FormLabel>Rating</FormLabel>
                            <StarRatingComponent
                                name="rating"
                                starCount={10}
                                editing={true}
                                value={rating}
                                onStarClick={(nextValue, prevValue, name) => {
                                    setRating(nextValue);
                                }}
                            />
                        </FormControl>

                        <FormControl>
                            <FormLabel>Review Text</FormLabel>
                            <Input
                                onChange={(e) => {
                                    setReviewText(e.currentTarget.value);
                                }}
                                placeholder="Review text"
                            />
                        </FormControl>
                        <ModalFooter>
                            <Button
                                onClick={() => {
                                    setRating(0);
                                    setReviewText('');
                                    onClose();
                                }}
                                colorScheme="Red"
                            >
                                Cancel
                            </Button>
                            <Button
                                colorScheme="green"
                                onClick={() => create()}
                            >
                                Save
                            </Button>
                        </ModalFooter>
                    </ModalBody>
                </ModalContent>
            </Modal>
        </>
    );
};
