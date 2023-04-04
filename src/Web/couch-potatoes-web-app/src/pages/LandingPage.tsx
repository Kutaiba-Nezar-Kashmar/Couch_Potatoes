import React from "react";
import {useNavigate} from "react-router-dom";

const LandingPage = () => {
    const navigate = useNavigate();
    const TEST_PAGE_URL = "/test";

    function navigateToTestPage() {
        navigate(TEST_PAGE_URL);
    }

    return (
        <>
            <div>Landing page works</div>

            <button
                className="transition ease-in-out delay-150 bg-blue-500 hover:-translate-y-1 hover:scale-110 hover:bg-indigo-500 duration-300 rounded px-4 py-2"
                onClick={navigateToTestPage}>
                Go to test page
            </button>
        </>
    )
};

export default LandingPage;