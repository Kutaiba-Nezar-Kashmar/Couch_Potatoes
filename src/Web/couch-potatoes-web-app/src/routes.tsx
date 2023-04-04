import {RouteObject} from "react-router-dom";
import LandingPage from "./pages/LandingPage";
import TestPage from "./pages/TestPage";

const routes: RouteObject[] = [
    {
        path: "/",
        element: <LandingPage/>
    },
    {
        path: "/test",
        element: <TestPage/>
    },
];

export default routes;