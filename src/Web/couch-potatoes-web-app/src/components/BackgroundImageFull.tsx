import {Box} from '@chakra-ui/react';
import React, {FC, ReactElement, useEffect, useState} from 'react';
import Navbar from './Navbar';
import User from '../models/user';
import {navBarVPaddingInRem, pageHPaddingInRem, pageVPaddingInRem} from "./settings/page-settings";

interface BackgroundImageProps {
    children?: React.ReactNode;
    imageUri?: string;
}

const BackgroundImageFull: FC<BackgroundImageProps> = ({children, imageUri}) => {


    return (
        <>
            <Box //TODO: This is not responsive. fix later
                backgroundImage={imageUri}
                backgroundSize="cover"
                backgroundRepeat="no-repeat"
                filter="brightness(50%)"
                display="inline"
                width="100%"
                height="100%"
                position="absolute"
                zIndex="-1"
                padding="0 0"
            >
            </Box>
            <Box zIndex="1" padding="0 0">
                {
                    children
                }
            </Box>
        </>
    );
};

export default BackgroundImageFull;
