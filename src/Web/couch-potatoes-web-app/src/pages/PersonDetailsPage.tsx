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

            setPerson(personData as PersonDetails);
            console.log(personData)
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