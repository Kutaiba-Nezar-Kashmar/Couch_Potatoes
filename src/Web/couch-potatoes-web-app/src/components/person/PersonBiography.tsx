import React, {FC, useEffect, useState} from 'react';
import {Flex, Heading, StackDivider, Text, VStack} from "@chakra-ui/react";

interface PersonBiographyInfo {
    name?: string;
    bio?: string;
}

const PersonBiography: FC<PersonBiographyInfo> =
    ({
         name,
         bio
     }) => {
        return (
            <VStack align="flex-start" spacing={4}>
                <Heading color="white" as='h3' size='lg'>
                    {name}
                </Heading>
                <Heading color="white" as='h4' size='md'>
                    Biography
                </Heading>
                <Text color="white">
                    {bio}
                </Text>
            </VStack>
        )
    }

export default PersonBiography;