export class FailedToRetrieveReviewsException extends Error {
    constructor(reason?: string) {
        super(`Failed to retrieve reviews: ${reason}`);
    }
}
