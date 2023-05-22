import React, {useEffect, useState} from 'react';
import {useFetchPersonDetailsAndCredits} from "../services/person-service/person-details-service";
import PersonDetails from "../models/person/person-details";
import {Box, Button, Flex, Grid, GridItem, SimpleGrid, Spinner} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import PersonSideBar from "../components/person/PersonSideBar";
import PersonBiography from "../components/person/PersonBiography";
import BasePage from "../components/BasePage";
import BackgroundImageFull from "../components/BackgroundImageFull";
import PersonMovieCredits from "../models/person/person-movie-credits";
import PersonMovieCreditsItem from "../components/person/PersonMovieCreditsItem";
import {Swiper, SwiperSlide} from "swiper/react";
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import {Navigation} from "swiper";
import {useNavigate, useParams} from "react-router-dom";
import PersonCreditsFilterBar from "../components/person/PersonCreditsFilterBar";
import CreditsAsCastDetails from "../components/person/CreditsAsCastDetails";
import CreditsAsCrewDetails from "../components/person/CreditsAsCrewDetails";
import PersonStats from "../models/person/person-stats";
import PersonStatsInformation from "../components/person/PersonStatsInformation";

//TODO: replace the Background_Temp to a proper placeholder.
const Background_Temp = 'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';

const PersonDetailsPage = () => {
    const navigate = useNavigate();
    const {personId} = useParams();
    const {
        isLoading,
        isError,
        data: personData,
        error
    } = useFetchPersonDetailsAndCredits({personId: (Number(personId))});
    const [person, setPerson] = useState<PersonDetails | null>(null);
    const [movieCredits, setMovieCredits] = useState<PersonMovieCredits | null>(null);
    const [personStats, setPersonStats] = useState<PersonStats | null>(null);
    const [selectedOption, setSelectedOption] = useState<string | null>("All");

    const handleSelectOption = (selectedOption: string) => {
        setSelectedOption(selectedOption);
    };

    useEffect(() => {
        if (!isLoading) {
            setPerson(personData?.details as PersonDetails);
            setMovieCredits(personData?.credits as PersonMovieCredits);
            setPersonStats(personData?.stats as PersonStats);
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
        console.log(personStats)
    }

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
                        <br/>
                        <PersonStatsInformation
                            numberOfMovies={personStats?.numberOfMovies}
                            averageMoviesRatingsAsACast={personStats?.averageMoviesRatingsAsACast}
                            averageMoviesRatingsAsACrew={personStats?.averageMoviesRatingsAsACrew}
                            knownForGenre={personStats?.knownForGenre}
                        />
                        <br/>
                        <Swiper
                            navigation={true}
                            modules={[Navigation]}
                            className="mySwiper"
                            slidesPerView={5}
                            pagination={{clickable: true}} spaceBetween={2}
                        >
                            {movieCredits?.creditsAsCast?.map(c =>
                                <SwiperSlide
                                    onClick={() => navigate(`movie/details/${c.movieId}`)}>
                                    <PersonMovieCreditsItem
                                        imageUri={getPosterImageUri(c.posterPath!) ?? Background_Temp}
                                        movieTitle={c.title as string}
                                    />
                                </SwiperSlide>)}
                        </Swiper>
                        <PersonCreditsFilterBar
                            actingCredits={movieCredits?.creditsAsCast.length}
                            crewCredits={movieCredits?.creditsAsCrew.length}
                            onSelectOption={handleSelectOption}
                        />
                        <br/>
                        <SimpleGrid spacing={4} templateRows='repeat(auto-fill, minmax(1fr))'>
                            {selectedOption === "All" ? movieCredits?.creditsAsCast
                                    .slice()
                                    .sort((a, b) => (new Date(a.releaseDate + "").getFullYear()) - (new Date(b.releaseDate + "").getFullYear()))
                                    .map(c =>
                                        <CreditsAsCastDetails title={c.title} releaseDate={new Date(c.releaseDate + "")}
                                                              character={c.character}/>)
                                    .concat(movieCredits?.creditsAsCrew
                                        .slice()
                                        .sort((a, b) => (new Date(a.releaseDate + "").getFullYear()) - (new Date(b.releaseDate + "").getFullYear()))
                                        .map(c =>
                                            <CreditsAsCrewDetails title={c.title} department={c.department} job={c.job}
                                                                  releaseDate={new Date(c.releaseDate + "")}/>)) :
                                <></>}
                            {selectedOption === "Acting" ? movieCredits?.creditsAsCast
                                    .slice()
                                    .sort((a, b) => (new Date(a.releaseDate + "").getFullYear()) - (new Date(b.releaseDate + "").getFullYear()))
                                    .map(c =>
                                        <CreditsAsCastDetails title={c.title} releaseDate={new Date(c.releaseDate + "")}
                                                              character={c.character}/>) :
                                <></>}
                            {selectedOption === "Production" ? movieCredits?.creditsAsCrew
                                    .slice()
                                    .sort((a, b) => (new Date(a.releaseDate + "").getFullYear()) - (new Date(b.releaseDate + "").getFullYear()))
                                    .map(c =>
                                        <CreditsAsCrewDetails title={c.title} department={c.department} job={c.job}
                                                              releaseDate={new Date(c.releaseDate + "")}/>) :
                                <></>}
                        </SimpleGrid>
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    )
}

export default PersonDetailsPage;