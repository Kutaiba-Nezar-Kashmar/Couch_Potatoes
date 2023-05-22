import React, {FC, useEffect, useState} from 'react';
import {Box, Card, CardBody, Heading, HStack, Text, VStack} from "@chakra-ui/react";

interface CrewDetails {
    title?: string;
    department?: string;
    job?: string;
    releaseDate?: Date;
}

const CreditsAsCrewDetails: FC<CrewDetails> = ({title, department, job, releaseDate}) => {
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
                            <Text>Department: {department}</Text>
                        </Box>
                        <Box>
                            <Text>as: {job}</Text>
                        </Box>
                    </VStack>
                </HStack>
            </CardBody>
        </Card>
    )
}

export default CreditsAsCrewDetails;