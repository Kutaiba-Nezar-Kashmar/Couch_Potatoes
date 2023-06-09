import { QueryClient, useMutation, useQuery } from 'react-query';
import { getConfig } from '../configuration/configuration';
import { auth } from '../firebase';
import User from '../models/user';
import { User as FirebaseUser, signOut } from 'firebase/auth';
import userEvent from '@testing-library/user-event';
import { stringify } from 'querystring';

export enum UserCacheKeys {
    GET_USER_WITH_ID = 'GET_USER_WITH_ID_',
    AUTHENTICATED_USER = 'AUTHENTICATED_USER',
}

export function useGetAuthenticatedUser() {
    return useQuery({
        queryKey: [UserCacheKeys.AUTHENTICATED_USER],
        queryFn: async () => {
            return await getAuthenticatedUser();
        },
        cacheTime: 0,
    });
}

export async function getAuthenticatedUser(): Promise<User | null> {
    const { currentUser } = auth;

    if (currentUser) {
        const user = domainUserFromFirebaseUser(currentUser);
        user.favoriteMovies = await getUserFavoriteMovieIds(user.id);
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
    const response = await fetch(`${config.baseUrl}/users/${userId}`);
    if (!response.ok) throw new Error('Something went wrong');
    const user: User = await response.json();
    return user.favoriteMovies ?? [];
}

export async function addFavoriteMovie(
    userId: string,
    movieId: number
): Promise<boolean> {
    const config = await getConfig();
    const body = { movieId: movieId };
    const options: RequestInit = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body),
    };
    const response = await fetch(
        `${config.baseUrl}/users/${userId}/favorites`,
        options
    );

    if (!response.ok) {
        return false;
    }

    return true;
}

export function useGetUserById(id?: string) {
    return useQuery({
        queryKey: [UserCacheKeys.GET_USER_WITH_ID + id],
        queryFn: async () => {
            if (!id) {
                return null;
            }
            return getUserById(id);
        },
    });
}

export async function getUserById(id: string): Promise<User | null> {
    const config = await getConfig();
    const response = await fetch(`${config.baseUrl}/users/${id}`);
    if (!response.ok) {
        return null;
    }

    const user: User = await response.json();
    if (!user.avatarUri) {
        user.avatarUri = `${process.env['PUBLIC_URL']}/blank-profile.png`;
    }

    return user;
}

export async function deleteFavoriteMovieForUser(
    userId: string,
    movieId: number
): Promise<boolean> {
    const config = await getConfig();
    const response = await fetch(
        `${config.baseUrl}/users/${userId}/favorites/${movieId}`,
        { method: 'DELETE', headers: { 'Content-Type': 'application/json' } }
    );

    if (!response.ok) {
        return false;
    }

    return true;
}

export async function getUsers(ids: string[]): Promise<User[]> {
    const config = await getConfig();
    const queryParams = ids.map((id) => `ids=${id}`).join('&');

    if (ids.length === 0) {
        return [];
    }

    const response = await fetch(`${config.baseUrl}/users?${queryParams}`);

    if (!response.ok) {
        throw new Error('Failed to fetch movies');
    }

    return response.json();
}

export async function signUserOut(
    onSignout: () => void,
    onError: (error: any) => void
) {
    signOut(auth)
        .then(() => {
            onSignout();
        })
        .catch((err) => {
            onError(err);
        });
}

export async function updateUser(
    userId: string,
    displayName: string,
    avatarUri: string
) {
    const config = await getConfig();
    const payload = {
        displayName: displayName,
        avatarUri: avatarUri,
    };

    const response = await fetch(`${config.baseUrl}/users/${userId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    });

    if (!response.ok) {
        throw new Error('Failed to update');
    }

    return await response.json();
}
