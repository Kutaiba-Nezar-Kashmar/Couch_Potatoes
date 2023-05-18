import React, {FC, ReactElement, useEffect, useState} from 'react';
import {Box, Image, VStack, StackDivider, Text, Heading, Grid, Flex, Spacer} from "@chakra-ui/react";

interface PersonProperties {
    uri?: string;
    alt?: string;
    known?: string;
    gender?: string;
    birthday?: Date;
    placeOfBirth?: string;
    aliases?: string[];
    homePage?: string;
}

const PersonSideBar: FC<PersonProperties> =
    ({
         uri,
         alt,
         known,
         gender,
         birthday,
         placeOfBirth,
         aliases,
         homePage
     }) => {
        return (
            <VStack spacing={10} align="stretch" padding="50">
                <Image
                    src={uri}
                    alt={alt}
                    w="20%"
                    borderRadius='20px'
                />
                <Box>
                    <Heading as='h4' size='md'>Home Page</Heading>
                    <Text>{homePage}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md'>Know For</Heading>
                    <Text>{known}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md'>Gender</Heading>
                    <Text>{gender}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md'>Birthday</Heading>
                    <Text>{birthday?.toString()}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md'>Place Of Birth</Heading>
                    <Text>{placeOfBirth}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md'>Aliases</Heading>
                    {aliases?.map(alias => <Text>{alias}</Text>)}
                </Box>
            </VStack>
        )
    }
export default PersonSideBar;
