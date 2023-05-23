import {RouteObject} from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import PersonDetailsPage from './pages/PersonDetailsPage';
import MovieDetailsPage from "./pages/MovieDetailsPage";
import LoginPage from './pages/LoginPage';
import Authenticated from './pages/Authenticated';
import ProfilePage from './pages/ProfilePage';
import FullCastAndCrewPage from "./pages/FullCastAndCrewPage";

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
        path: '/login',
        element: <LoginPage />,
    },
    {
        path: '/authenticated',
        element: <Authenticated />,
    },
    {
        path: '/profile',
        element: <ProfilePage />,
    },
    {
        path: '/profile/:userId',
        element: <ProfilePage />,
    },
    {
        path: '/person/:personId',
        element: <PersonDetailsPage/>
    },
    {
        path: '/movie/details/:movieId',
        element: <MovieDetailsPage/>,
    },
    {
        path: '/movie/:movieId/cast',
        element: <FullCastAndCrewPage/>
    }
];

export default routes;
