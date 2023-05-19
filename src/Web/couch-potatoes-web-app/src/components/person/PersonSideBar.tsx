import React, {FC, ReactElement, useEffect, useState} from 'react';
import {Box, Image, VStack, StackDivider, Text, Heading, Grid, Flex, Spacer, Stack} from "@chakra-ui/react";

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
            <VStack align="flex-start" spacing={4}>
                <Image src={uri} alt={alt} borderRadius='20px'/>
                <Box>
                    <Heading color="white" as='h4' size='md'>Home Page</Heading>
                    <Text>{homePage}</Text>
                </Box>
                <Box>
                    <Heading color="white" as='h4' size='md'>Know For</Heading>
                    <Text color="white">{known}</Text>
                </Box>
                <Box>
                    <Heading color="white" as='h4' size='md'>Gender</Heading>
                    <Text color="white">{gender}</Text>
                </Box>
                <Box>
                    <Heading color="white" as='h4' size='md'>Birthday</Heading>
                    <Text color="white">{birthday?.toString()}</Text>
                </Box>
                <Box>
                    <Heading color="white" as='h4' size='md'>Place Of Birth</Heading>
                    <Text color="white">{placeOfBirth}</Text>
                </Box>
                <Box>
                    <Heading as='h4' size='md' color="white">Aliases</Heading>
                    {aliases?.map(alias => <Text color="white">{alias}</Text>)}
                </Box>

            </VStack>
        )
    }
export default PersonSideBar;
