import React, {useState} from 'react';
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
    const [backgroundImage, setBackgroundImage] = useState("/default-image.jpg");
    const Background_Temp =
        'https://static1.cbrimages.com/wordpress/wp-content/uploads/2023/02/john-wick-4-paris-poster.jpg';
    // Function to handle background image change
    const handleBackgroundChange = () => {
        setBackgroundImage(Background_Temp);
    };
    return (
        <>
            <QueryClientProvider client={queryClient}>
                <ChakraProvider resetCSS={true} theme={theme}>
                    <div  style={{
                        backgroundImage: `url(${backgroundImage})`,
                        backgroundSize: "cover",
                        backgroundPosition: "center",
                        maxWidth: "100vw",
                        minHeight: "100vh",
                    }}>
                        <RouterProvider router={router} />
                    </div>

                </ChakraProvider>
            </QueryClientProvider>
        </>
    );
};

export default App;
