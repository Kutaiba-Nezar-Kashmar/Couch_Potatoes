import { RouteObject } from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import TestPage from './pages/TestPage';
import PersonDetailsPage from './pages/PersonDetailsPage';

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
        path: '/person',
        element: <PersonDetailsPage/>
    }
];

export default routes;
