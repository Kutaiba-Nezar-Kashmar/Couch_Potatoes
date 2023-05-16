import cors from 'cors';
import bodyParser from 'body-parser';
import { Express } from 'express';
export function applyMiddlewares(server: Express) {
    server.use(cors());
    server.use(bodyParser.json());
}
