import React, {FC, ReactElement, useEffect, useState} from 'react';
import {Box, Image} from "@chakra-ui/react";

interface PersonImageProperties {
    uri?: string;
    alt?: string
}

const PersonImage: FC<PersonImageProperties> = ({uri, alt}) => {
    return(
        <>
            <Box boxSize='sm'>
                <Image src={uri} alt={alt}/>
            </Box>
        </>
    )
}
export default PersonImage;