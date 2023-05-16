import { Express } from 'express';

export interface IController {
    apply(server: Express): void;
}
