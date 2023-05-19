import { useQuery } from 'react-query';
import { getConfig } from '../configuration/configuration';
import { auth } from '../firebase';
import User from '../models/user';
import { User as FirebaseUser } from 'firebase/auth';

export async function getUser(): Promise<User | null> {
    const { currentUser } = auth;

    if (currentUser) {
        const user = domainUserFromFirebaseUser(currentUser);
        user.favoriteMovies = await getUserFavoriteMovieIds(user.id);
        return user;
    }

    const userJsonFromLocalStorage = localStorage.getItem('currentUser');
    // NOTE: (mibui 2023-05-19) we check for the string 'undefined' instead of actual undefined
    //                          since that is the behaviour of localStorage::getItem
    if (userJsonFromLocalStorage && userJsonFromLocalStorage !== 'undefined') {
        const user = JSON.parse(userJsonFromLocalStorage);
        return user;
    }

    return null;
}

export function domainUserFromFirebaseUser(
    user: FirebaseUser | firebase.default.User
): User {
    return {
        avatarUri:
            user.photoURL ?? `${process.env['PUBLIC_URL']}/blank-profile.png`,
        displayName: user.displayName,
        email: user.email,
        id: user.uid,
        createdTimestamp: user.metadata.creationTime
            ? new Date(user.metadata.creationTime)
            : null,
        lastSignInTimestamp: user.metadata.lastSignInTime
            ? new Date(user.metadata.lastSignInTime)
            : null,
        favoriteMovies: [],
    };
}

export async function getUserFavoriteMovieIds(
    userId: string
): Promise<number[]> {
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}v1/users/${userId}`);
    if (!response.ok) throw new Error('Something went wrong');
    const user: User = await response.json();
    return user.favoriteMovies ?? [];
}
