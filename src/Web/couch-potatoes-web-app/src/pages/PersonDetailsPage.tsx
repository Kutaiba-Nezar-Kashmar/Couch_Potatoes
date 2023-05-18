import React, {useEffect, useState} from 'react';
import SampleComponent from '../components/SampleComponent';
import BasePage from '../components/BasePage';

import {useFetchPersonDetails} from "../services/person-service/person-details-service";
import Movie from "../models/movie";
import PersonDetails from "../models/person/person-details";

const PersonDetailsPage = () => {
    const {isLoading, isError, data: personData, error} = useFetchPersonDetails({personId: 2});
    const [person, setPerson] = useState<PersonDetails | null>(null);

    useEffect(() => {
        if (!isLoading) {
            // NOTE: (mibui 2023-05-15) Take the most popular movie as featured.
            // TODO: (mibui 2023-05-15) Fetch from /movies/:movieid instead of taking from collection.
            //                          e.g. setMovie(getMovie((collections as any)!["popular"]["collection"][0]?.id))
            setPerson(personData as PersonDetails);
            console.log(personData)// TODO: Remove the genres, they are only there for testing purposes
        }
    }, [isLoading])

    return (
        <BasePage>
            <div>

            </div>
        </BasePage>
    )
}

export default PersonDetailsPage;