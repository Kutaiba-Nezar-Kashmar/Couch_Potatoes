import {RouteObject} from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import PersonDetailsPage from './pages/PersonDetailsPage';
import MovieDetailsPage from "./pages/MovieDetailsPage";

const routes: RouteObject[] = [
    {
        path: '/',
        element: <IndexPage/>,
    },
    {
        path: '/test',
        element: <TestPage/>,
    },
    {
        path: '/person',
        element: <PersonDetailsPage/>
    }
    {
        path: '/details',
        element: <MovieDetailsPage/>,
    }
];

export default routes;
