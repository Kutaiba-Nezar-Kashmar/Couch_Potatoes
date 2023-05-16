import { Result, ResultType, voidData } from '../common/monads';
import { FailedToCreateReviewException } from '../exceptions/failed-to-create-review';
import { UserDoesNotExistException } from '../exceptions/user-does-not-exists';
import { Review } from '../models/review';
import getFirestore from './firebase';

export interface IReviewRepository {
    getAllForUser(userId: string): Promise<Result<Review[], UserDoesNotExistException>>;

    getAllForMovie(movieId: number): Promise<Result<Review[], Error>>;

    create(review: Review): Promise<Result<void, FailedToCreateReviewException>>;
}

export class FirebaseReviewRepository implements IReviewRepository {
    private db: FirebaseFirestore.Firestore;
    private COLLECTION_NAME = 'Reviews';

    constructor() {
        this.db = getFirestore();
    }

    async getAllForMovie(movieId: number): Promise<Result<Review[], Error>> {
        try {
            const reviewsRef = this.db.collection(this.COLLECTION_NAME).doc(movieId.toString());
            const reviews: Review[] = (await reviewsRef.get()).data() as Review[];
            return { type: ResultType.OK, data: reviews };
        } catch (err) {
            return { type: ResultType.ERROR, error: new Error(err) };
        }
    }

    async getAllForUser(userId: string): Promise<Result<Review[], UserDoesNotExistException>> {
        userId;
        throw new Error('Method not implemented');
    }

    async create(review: Review): Promise<Result<void, FailedToCreateReviewException>> {
        try {
            const reviewsDoc = await this.db
                .collection(this.COLLECTION_NAME)
                .doc(review.movieId.toString())
                .get();

            if (!reviewsDoc.exists) {
                await reviewsDoc.ref.set({ reviews: [] });
            }

            reviewsDoc.ref.set({ reviews: [review] }, { merge: true });

            return { type: ResultType.OK, data: voidData() };
        } catch (err) {
            return {
                type: ResultType.ERROR,
                error: new FailedToCreateReviewException(`${err}`),
            };
        }
    }
}
