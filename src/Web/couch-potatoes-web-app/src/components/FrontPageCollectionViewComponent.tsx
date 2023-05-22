import {Box, Button, Card, CardBody, Flex, Heading, Image, Stack, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import React, {FC} from "react";
import MovieCredits from "../models/movie_credits";
import MovieRecommendations from "../models/movie-Recommedations";
import {useNavigate} from "react-router-dom";
// Import Swiper React components
import {Swiper, SwiperSlide} from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/effect-coverflow";
import "swiper/css/pagination";
// import required modules
import {EffectCoverflow, Pagination} from "swiper";


export interface MovieCollectionComponentProps {
    movieCollection: MovieRecommendations,
}

export const FrontPageCollectionViewComponent: FC<MovieCollectionComponentProps> = ({movieCollection}) => {
    const navigate = useNavigate();
    return (
        <>
            <Swiper
                autoplay={true}
                effect={"coverflow"}
                grabCursor={true}
                direction="horizontal"
                centeredSlides={true}
                slidesPerView={7}
                loop={true}
                mousewheel={true}
                coverflowEffect={{
                    rotate: 10,
                    stretch: 0,
                    depth: 120,
                    modifier: 1,
                    slideShadows: true,

                }}
                pagination={true}
                modules={[EffectCoverflow, Pagination]}
                className="mySwiper"
            >
                {movieCollection && movieCollection?.collection.map((movie) => (
                    <SwiperSlide onClick={() => navigate(`/movie/details/${movie.id}`)}>
                        <Image maxWidth={225}
                               src={getPosterImageUri(movie?.imageUri as string)}
                               alt='No image available of actor'
                               borderRadius='lg'
                        />

                    </SwiperSlide>
                ))}


            </Swiper>


        </>
    )
}