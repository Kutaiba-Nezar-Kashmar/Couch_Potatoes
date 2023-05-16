export class FailedToCreateReviewException extends Error {
    constructor(public reason?: string) {
        super(`Failed to create review: ${reason ?? ""}`);
    }
}
