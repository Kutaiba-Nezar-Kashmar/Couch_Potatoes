import {
    Box,
    Button,
    Card,
    CardBody,
    Flex,
    Heading,
    Image,
    Stack,
    Text,
    Tooltip,
} from '@chakra-ui/react';
import { getPosterImageUri } from '../services/images';
import React, { FC } from 'react';
import MovieCredits from '../models/movie_credits';
import MovieRecommendations from '../models/movie-Recommedations';
import { useNavigate } from 'react-router-dom';
// Import Swiper React components
import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/effect-coverflow';
import 'swiper/css/pagination';
// import required modules
import { EffectCoverflow, FreeMode, Navigation, Pagination } from 'swiper';
import { sliceNumber } from '../util/numberutil';

export interface MovieCollectionComponentProps {
    movieCollection: MovieRecommendations;
}

export const FrontPageCollectionViewComponent: FC<
    MovieCollectionComponentProps
> = ({ movieCollection }) => {
    const navigate = useNavigate();
    return (
        <>
            <Swiper
                autoplay={true}
                effect={'coverflow'}
                grabCursor={true}
                navigation={true}
                direction="horizontal"
                centeredSlides={true}
                slidesPerView={7}
                loop={true}
                freeMode={true}
                mousewheel={true}
                coverflowEffect={{
                    rotate: 10,
                    stretch: 0,
                    depth: 120,
                    modifier: 1,
                    slideShadows: true,
                }}
                modules={[EffectCoverflow, Navigation, FreeMode]}
                className="mySwiper"
            >
                {movieCollection &&
                    movieCollection?.collection.map((movie) => (
                        <SwiperSlide
                            onClick={() =>
                                navigate(
                                    `/Couch_Potatoes/movie/details/${movie.id}`
                                )
                            }
                        >
                            <Tooltip
                                label={
                                    'Rating: ' +
                                    sliceNumber(movie?.tmdbScore, 1)
                                }
                                placement="top"
                            >
                                <Image
                                    maxWidth={225}
                                    src={getPosterImageUri(
                                        movie?.imageUri as string
                                    )}
                                    alt={'poster of movie ' + movie.title}
                                    borderRadius="lg"
                                    filter="brightness(0.9)"
                                    _hover={{ filter: 'brightness(1.1)',cursor: "pointer" }}
                                />
                            </Tooltip>
                        </SwiperSlide>
                    ))}
            </Swiper>
        </>
    );
};
