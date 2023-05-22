import React, {useState, useEffect, FC} from 'react';
import {Grid, GridItem, Heading, HStack, Select, Stack, Text} from "@chakra-ui/react";

interface PersonCreditsFilterProperties {
    actingCredits?: number,
    crewCredits?: number,
    isActing?: boolean
}

const PersonCreditsFilterBar: FC<PersonCreditsFilterProperties> = ({
                                                                       actingCredits,
                                                                       crewCredits,
                                                                       isActing
                                                                   }) => {
    return (
        <Grid templateColumns='repeat(3, 1fr)' gap={6}>
            <GridItem colSpan={2}>
                <Heading color="white" as='h4' size='md'>Credits</Heading>
            </GridItem>
            <GridItem colSpan={1}>
                <HStack spacing={2}>
                    <Select backgroundColor='white'>
                        <option>All {(actingCredits ?? 0) + (crewCredits ?? 0)}</option>
                        <option>Acting {actingCredits ?? 0}</option>
                        <option>Production {crewCredits ?? 0}</option>
                    </Select>
                </HStack>
            </GridItem>
        </Grid>
    )
}
export default PersonCreditsFilterBar;