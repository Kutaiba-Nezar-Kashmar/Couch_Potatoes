import React from 'react';
import { ChakraProvider, extendTheme } from '@chakra-ui/react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import routes from './routes';
import Navbar from './components/Navbar';

const router = createBrowserRouter(routes);
const theme = extendTheme({
    fonts: {
        heading: `'Roboto', 'Helvetica Neue', sans-serif`,
        body: `'Roboto', 'Helvetica Neue', sans-serif`,
    },
});

const App = () => {
    return (
        <>
            <ChakraProvider resetCSS={true} theme={theme}>
                <RouterProvider router={router} />
            </ChakraProvider>
        </>
    );
};

export default App;
