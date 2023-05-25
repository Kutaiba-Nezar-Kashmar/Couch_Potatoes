import { EventListener } from './event-listener';
import { EventEmitter } from './event-emitter';

export let removeFavoriteMovieEmitter: EventEmitter;

export function subscribeRemoveFavoriteMovieEventListener(
    listener: EventListener
) {
    removeFavoriteMovieEmitter.subscribe(listener);
}

export function emitRemoveFavoriteMovieEvent(data: number) {
    removeFavoriteMovieEmitter.emit(data);
}

export function unsubscribeRemoveFavoriteMovieEvent(listener: EventListener) {
    removeFavoriteMovieEmitter.unsubscribe(listener);
}

export default function initFavoriteMovieEmitter() {
    removeFavoriteMovieEmitter = new EventEmitter();
}
