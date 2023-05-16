export class FailedToRetrieveReviewsException extends Error {
    constructor(public reason?: string) {
        super(`Failed to retrieve reviews: ${reason ?? ""}`);
    }
}
