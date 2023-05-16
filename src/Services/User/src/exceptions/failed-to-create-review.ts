export class FailedToCreateReviewException extends Error {
    constructor(public reason?: string) {
        super(reason);
    }
}
