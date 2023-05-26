import React, {FC} from 'react';
import {Heading, Text, VStack} from "@chakra-ui/react";

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
                    {name ? (<Text color="white">{name}</Text>) : (<Text color="white">N/A</Text>)}
                </Heading>
                <Heading color="white" as='h4' size='md'>
                    Biography
                </Heading>
                <Text color="white">
                    {bio ? (<Text color="white">{bio}</Text>) : (<Text color="white">N/A</Text>)}
                </Text>
            </VStack>
        )
    }

export default PersonBiography;