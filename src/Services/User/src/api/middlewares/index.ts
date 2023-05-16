import cors from 'cors';
import bodyParser from 'body-parser';
import { Express } from 'express';

export default function apply(server: Express) {
    console.log(server);
    server.use(cors());
    server.use(bodyParser.json());
}
