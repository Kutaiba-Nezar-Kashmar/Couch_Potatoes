export default interface User {
    displayName: string | null;
    avatarUri: string | null;
    email: string | null;
    id: string;
    createdTimestamp: Date | null;
    lastSignInTimestamp: Date | null;
    favoriteMovies?: number[];
}
