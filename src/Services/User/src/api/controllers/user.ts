import { Express } from 'express';
import { IController } from './route';

export default class UserRoute implements IController {
    apply(server: Express) {}
}
