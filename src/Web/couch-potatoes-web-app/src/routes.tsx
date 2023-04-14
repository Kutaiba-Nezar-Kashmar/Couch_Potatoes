import { RouteObject } from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';

const routes: RouteObject[] = [
    {
        path: '/',
        element: <IndexPage />,
    },
    {
        path: '/test',
        element: <TestPage />,
    },
];

export default routes;
