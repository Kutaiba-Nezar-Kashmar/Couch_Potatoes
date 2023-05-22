import { EventListener } from './event-listener';

export class EventEmitter {
    listeners: EventListener[] = [];

    constructor() {
        this.listeners = [];
    }
    public emit(data: any): void {
        this.listeners.forEach((listener) => listener.onEvent(data));
    }

    public subscribe(listener: EventListener) {
        this.listeners.push(listener);
    }

    public unsubscribe(listener: EventListener) {
        this.listeners = this.listeners.filter((l) => l !== listener);
    }
}
