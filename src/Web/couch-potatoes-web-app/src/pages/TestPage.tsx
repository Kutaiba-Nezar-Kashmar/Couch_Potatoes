import React from 'react';
import SampleComponent from '../components/SampleComponent';
import BasePage from '../components/BasePage';

const TestPage = () => {
    return (
        <>
            <BasePage>
                <div>Test page works</div>
                <SampleComponent startCount={1} />
            </BasePage>
        </>
    );
};

export default TestPage;
