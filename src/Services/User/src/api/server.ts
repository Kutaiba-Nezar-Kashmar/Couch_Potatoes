import express from 'express';
import middlewares from './middlewares/index';
import routes from './controllers/index';

const server = express();
const PORT = Number(process.env['PORT']) || 8083;

middlewares.apply(server);
routes.apply(server);

const startServer = () => {
    server.listen(PORT, () => {
        `Server started listening on port ${PORT}`;
    });
};

export default startServer;
