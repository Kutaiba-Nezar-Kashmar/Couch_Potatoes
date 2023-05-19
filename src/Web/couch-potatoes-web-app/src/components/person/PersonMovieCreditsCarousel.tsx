import React, {useState, useEffect, FC} from 'react';
import {Carousel} from 'react-responsive-carousel';
import {Box, Image} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";
import PersonCreditsCarouselItem, {PersonMovieCreditsProperties} from "./PersonCreditsCarouselItem";
import Cast from "../../models/person/cast";


interface PersonCredits {
    credits: Cast[]
}

//TODO: set the onClick on the image to navigate to the movie page with that id
const PersonMovieCreditsCarousel: FC<PersonCredits> = ({
                                                           credits
                                                       }) => {
    return (
        <>
            <Carousel infiniteLoop={true} autoPlay={true} interval={3000}
                      stopOnHover={true} dynamicHeight={true}>
                {credits.map(c => <PersonCreditsCarouselItem uri={c.backdropPath} title={c.OriginalTitle}/>)}
            </Carousel>
        </>
    )
}

export default PersonMovieCreditsCarousel;