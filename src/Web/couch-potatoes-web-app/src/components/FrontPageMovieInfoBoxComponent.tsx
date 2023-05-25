import {Flex, HStack, Text, VStack} from "@chakra-ui/react";
import StarRatingComponent from "react-star-rating-component";
import React, {FC} from "react";
import Movie from "../models/movie";
// Import Swiper React components
import {Swiper, SwiperSlide} from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/effect-coverflow";
import "swiper/css/pagination";
// import required modules
import {EffectCoverflow, Pagination} from "swiper";


export interface FrontPageMovieInfoBoxComponentProps {
    movie: Movie | null;
}


const FrontPageMovieInfoBoxComponent: FC<FrontPageMovieInfoBoxComponentProps> = ({movie}) => {
    return (
        <>

            <VStack>
                <Text marginTop="1rem" textColor="white" fontSize={{base: 'xl', md: '2xl', lg: '3xl'}}
                      textTransform="uppercase">
                    {movie?.title}
                </Text>
                <Flex width="100%" justifyContent="center" alignItems="center">

                </Flex>
                <Text textColor="white" fontSize={{base: 'lg', md: 'xl', lg: '2xl'}}>
                    {movie?.releaseDate ? ( new Date(movie?.releaseDate as string).toLocaleDateString()):("N/A")}

                </Text>
                <StarRatingComponent name="rating" value={movie?.tmdbScore || 0} starCount={10}/>
            </VStack></>
    )
};

export default FrontPageMovieInfoBoxComponent;