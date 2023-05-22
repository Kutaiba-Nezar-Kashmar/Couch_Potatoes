import React, {useState, useEffect, FC} from 'react';
import {Grid, GridItem, Heading, HStack, Select, Stack, Text} from "@chakra-ui/react";

interface PersonCreditsFilterProperties {
    actingCredits?: number,
    crewCredits?: number,
    onSelectOption: (selectedOption: string) => void;
}

const PersonCreditsFilterBar: FC<PersonCreditsFilterProperties> = ({
                                                                       actingCredits,
                                                                       crewCredits,
                                                                       onSelectOption
                                                                   }) => {
    const handleOptionChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOption = event.target.value;
        onSelectOption(selectedOption);
    };

    return (
        <Grid templateColumns='repeat(3, 1fr)' gap={6}>
            <GridItem colSpan={2}>
                <Heading color="white" as='h4' size='md'>Credits</Heading>
            </GridItem>
            <GridItem colSpan={1}>
                <HStack spacing={2}>
                    <Select backgroundColor='white' onChange={handleOptionChange}>
                        <option value="All">All {(actingCredits ?? 0) + (crewCredits ?? 0)}</option>
                        <option value="Acting">Acting {actingCredits ?? 0}</option>
                        <option value="Production">Production {crewCredits ?? 0}</option>
                    </Select>
                </HStack>
            </GridItem>
        </Grid>
    )
}
export default PersonCreditsFilterBar;