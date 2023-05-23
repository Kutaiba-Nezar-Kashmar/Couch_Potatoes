import { EventListener } from './event-listener';
import { EventEmitter } from './event-emitter';

export let removeReview: EventEmitter;

export function subscribeRemoveReviewEmitter(listener: EventListener) {
    removeReview.subscribe(listener);
}

export function emitReviewRemoved(data: string) {
    removeReview.emit(data);
}

export function unsubscribeRemoveReview(listener: EventListener) {
    removeReview.unsubscribe(listener);
}

export default function initRemoveReviewEmitter() {
    removeReview = new EventEmitter();
}
