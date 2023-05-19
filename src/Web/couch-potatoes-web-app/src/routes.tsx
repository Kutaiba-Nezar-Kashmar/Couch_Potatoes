import { RouteObject } from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import LoginPage from './pages/LoginPage';
import Authenticated from './pages/Authenticated';
import ProfilePage from './pages/ProfilePage';

const routes: RouteObject[] = [
    {
        path: '/',
        element: <IndexPage />,
    },
    {
        path: '/test',
        element: <TestPage />,
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
];

export default routes;
