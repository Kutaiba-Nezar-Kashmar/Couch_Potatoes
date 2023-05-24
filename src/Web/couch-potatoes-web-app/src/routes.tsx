import { RouteObject } from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import PersonDetailsPage from './pages/PersonDetailsPage';
import MovieDetailsPage from './pages/MovieDetailsPage';
import LoginPage from './pages/LoginPage';
import Authenticated from './pages/Authenticated';
import ProfilePage from './pages/ProfilePage';
import FullCastAndCrewPage from './pages/FullCastAndCrewPage';

const routes: RouteObject[] = [
    {
        path: '/Couch_Potatoes',
        element: <IndexPage />,
    },
    {
        path: '/Couch_Potatoes/login',
        element: <LoginPage />,
    },
    {
        path: '/Couch_Potatoes/authenticated',
        element: <Authenticated />,
    },
    {
        path: '/Couch_Potatoes/profile',
        element: <ProfilePage />,
    },
    {
        path: '/Couch_Potatoes/profile/:userId',
        element: <ProfilePage />,
    },
    {
        path: '/Couch_Potatoes/person/:personId',
        element: <PersonDetailsPage />,
    },
    {
        path: '/Couch_Potatoes/movie/details/:movieId',
        element: <MovieDetailsPage />,
    },
    {
        path: '/Couch_Potatoes//movie/:movieId/cast',
        element: <FullCastAndCrewPage />,
    },
];

export default routes;
