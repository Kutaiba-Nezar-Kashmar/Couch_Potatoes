import React, {useState, useEffect, FC} from 'react';
import {Box, Image, Stack, StackDivider, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";


export interface PersonMovieCreditsProperties {
    uri?: string;
    title?: string;
}

const PersonCreditsCarouselItem: FC<PersonMovieCreditsProperties> = ({uri, title}) => {
    return (
        <Stack divider={<StackDivider/>} spacing='4' paddingTop={4}>
            <Box>
                <Image
                    src={getPosterImageUri(uri!)} alt={title}
                    width="100%"
                    maxHeight="500px"
                    height="auto"
                    objectFit="contain"/>
            </Box>
            <Box>
                <Text color="white">{title}</Text>
            </Box>
        </Stack>
    )
}

export default PersonCreditsCarouselItem;