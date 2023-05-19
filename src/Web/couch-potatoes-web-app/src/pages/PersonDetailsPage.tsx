import React, {useEffect, useState} from 'react';
import {useFetchPersonDetailsAndCredits} from "../services/person-service/person-details-service";
import PersonDetails from "../models/person/person-details";
import {Box, Flex, Grid, GridItem, Spinner} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import PersonSideBar from "../components/person/PersonSideBar";
import PersonBiography from "../components/person/PersonBiography";
import BasePage from "../components/BasePage";
import BackgroundImageFull from "../components/BackgroundImageFull";
import PersonMovieCredits from "../models/person/person-movie-credits";
import PersonMovieCreditsCarousel from "../components/person/PersonMovieCreditsCarousel";
import {PersonMovieCreditsProperties} from "../components/person/PersonCreditsCarouselItem";

//TODO: replace the Background_Temp to a proper placeholder.
const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

const PersonDetailsPage = () => {
    const {isLoading, isError, data: personData, error} = useFetchPersonDetailsAndCredits({personId: 2});
    const [person, setPerson] = useState<PersonDetails | null>(null);
    const [movieCredits, setMovieCredits] = useState<PersonMovieCredits | null>(null);

    useEffect(() => {
        if (!isLoading) {
            setPerson(personData?.details as PersonDetails);
            setMovieCredits(personData?.credits as PersonMovieCredits);
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
        console.log(movieCredits)
    }
//TODO: change to list of cads instead of a carousel
    return (
        <BackgroundImageFull imageUri={Background_Temp}>
            <BasePage>
                <Grid templateColumns='repeat(4, 1fr)' gap={4} paddingTop={5}>
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
                <PersonMovieCreditsCarousel credits={movieCredits?.creditsAsCast??[]}/>
            </BasePage>
        </BackgroundImageFull>
    )
}

export default PersonDetailsPage;