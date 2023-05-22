import React, {FC, useEffect, useState} from 'react';
import {Box, Card, CardBody, Heading, HStack, Text, VStack} from "@chakra-ui/react";

interface CastDetails {
    title?: string;
    releaseDate?: Date;
    character?: string;
}

const CreditsAsCastDetails: FC<CastDetails> = ({title, releaseDate, character}) => {
    return (
        <Card backgroundColor='white'>
            <CardBody>
                <HStack spacing={100}>
                    <Box>
                        <Text>{releaseDate?.getFullYear() ?? "-"}</Text>
                    </Box>
                    <VStack spacing={2} display='flex' alignItems='left'>
                        <Box>
                            <Heading as='h4' size='sm'>{title ?? "UNKNOWN"}</Heading>
                        </Box>
                        <Box>
                            <Text>as: {character ?? "-"}</Text>
                        </Box>
                    </VStack>
                </HStack>
            </CardBody>
        </Card>
    )
}

export default CreditsAsCastDetails;