export interface Review {
    movieId: number;
    userId: string;
    rating: number;
    reviewText: string;
    creationDate: Date;
    votes: ReviewVote[];
}

export enum ReviewVoteDirection {
    UPVOTE = "Upvote",
    DOWNVOTE = "Downvote",
}

export interface ReviewVote {
    direction: ReviewVoteDirection;
    userId: string;
}
