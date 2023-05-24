import React from 'react';
import { ChakraProvider, extendTheme } from '@chakra-ui/react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import routes from './routes';
import Navbar from './components/Navbar';
import { QueryClient, QueryClientProvider } from 'react-query';
import initFavoriteMovieEmitter from './services/event-emitters/favorite-movie-emitter';
import initRemoveReviewEmitter from './services/event-emitters/review-emitter';

// Create a client
const queryClient = new QueryClient();

const router = createBrowserRouter(routes);

const theme = extendTheme({
    fonts: {
        heading: `'Roboto', 'Helvetica Neue', sans-serif`,
        body: `'Roboto', 'Helvetica Neue', sans-serif`,
    },
});


initFavoriteMovieEmitter();
initRemoveReviewEmitter();

const App = () => {
    return (
        <>
            <QueryClientProvider client={queryClient}>
                <ChakraProvider resetCSS={true} theme={theme}>
                    <RouterProvider router={router} />
                </ChakraProvider>
            </QueryClientProvider>
        </>
    );
};

export default App;
