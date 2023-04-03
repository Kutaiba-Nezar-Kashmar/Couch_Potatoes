import React from "react";
import SampleComponent from "../components/SampleComponent";

const TestPage = () => {
    return (
        <>
            <div>Test page works</div>
            <SampleComponent startCount={1}/>
        </>
    )
};

export default TestPage;
