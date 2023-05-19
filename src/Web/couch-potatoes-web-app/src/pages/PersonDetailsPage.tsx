import React, {useEffect, useState} from 'react';
import {useFetchPersonDetails} from "../services/person-service/person-details-service";
import PersonDetails from "../models/person/person-details";
import {Box, Flex, Grid, GridItem, Spinner} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import PersonSideBar from "../components/person/PersonSideBar";
import PersonBiography from "../components/person/PersonBiography";
import BasePage from "../components/BasePage";
import BackgroundImageFull from "../components/BackgroundImageFull";

//TODO: replace the Background_Temp to a proper placeholder.
const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

const PersonDetailsPage = () => {
    const {isLoading, isError, data: personData, error} = useFetchPersonDetails({personId: 2});
    const [person, setPerson] = useState<PersonDetails | null>(null);

    useEffect(() => {
        if (!isLoading) {
            setPerson(personData as PersonDetails);
        }
    }, [isLoading])

    if (isLoading) {
        return <Flex width="100%" height="100%" justifyContent="center" alignItems="center">
            <Spinner
                thickness='4px'
                speed='0.65s'
                emptyColor='gray.200'
                color='blue.500'
                size='xl'
            />
        </Flex>
    }

    if (isError) {
        console.log(error);
    }

    if (personData) {
        console.log(person)
    }

    return (
        <BackgroundImageFull imageUri={Background_Temp}>
            <BasePage>
                {/*<Box minHeight="800vh" display="flex" justifyContent="center" alignItems="center" position="absolute">*/}
                    <Grid templateColumns='repeat(4, 1fr)' gap={4}  paddingTop={5}>
                        <GridItem colSpan={1}>
                            <PersonSideBar
                                uri={getPosterImageUri(person?.profilePath as string) || Background_Temp}
                                alt={person?.name || 'temp'}
                                known={person?.knownForDepartment}
                                gender={person?.gender}
                                birthday={person?.birthday}
                                placeOfBirth={person?.placeOfBirth}
                                aliases={person?.aliases}
                                homePage={person?.homepage}
                            />
                        </GridItem>
                        <GridItem colSpan={3}>
                            <PersonBiography name={person?.name} bio={person?.biography}/>
                        </GridItem>
                    </Grid>
                {/*</Box>*/}
            </BasePage>
        </BackgroundImageFull>
    )
}

export default PersonDetailsPage;