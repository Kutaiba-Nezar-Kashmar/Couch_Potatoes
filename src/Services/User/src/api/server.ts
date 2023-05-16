import express from 'express';
import {applyMiddlewares} from './middlewares/index';
import {applyControllers} from './controllers/index';

export const server = express();
export const PORT = Number(process.env['PORT']) || 8083;

applyMiddlewares(server);
applyControllers(server);
export const startServer = () => {
    server.listen(PORT, () => {
        console.log(`Server started listening on port ${PORT}`);
    });
};
