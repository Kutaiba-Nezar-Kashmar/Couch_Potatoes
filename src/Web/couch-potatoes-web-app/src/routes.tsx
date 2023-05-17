import {RouteObject} from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
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
        path: '/details',
        element: <MovieDetailsPage/>,
    }
];

export default routes;
