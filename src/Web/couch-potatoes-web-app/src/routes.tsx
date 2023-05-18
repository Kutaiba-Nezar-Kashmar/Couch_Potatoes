import { RouteObject } from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import LoginPage from './pages/LoginPage';

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
];

export default routes;
