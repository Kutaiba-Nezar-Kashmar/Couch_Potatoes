import { Box } from '@chakra-ui/react';
import React, { FC, ReactElement, useEffect, useState } from 'react';
import Navbar from './Navbar';
import User from '../models/user';
import {
    navBarVPaddingInRem,
    pageHPaddingInRem,
    pageVPaddingInRem,
} from './settings/page-settings';
import { auth } from '../firebase';
import { onAuthStateChanged } from 'firebase/auth';
import firebase from 'firebase/compat';
import { domainUserFromFirebaseUser, getUser } from '../services/user';

interface BasePageProps {
    children?: React.ReactNode;
    styles?: React.CSSProperties;
}

const BasePage: FC<BasePageProps> = ({ children, styles }) => {
    const [user, setUser] = useState<User | null>(null);

    async function getUserIfLoggedIn(): Promise<void> {
        // TODO: (mibui 2023-04-14) Implement this
        const currentUser = await getUser();
        if (user) {
            setUser(currentUser);
            return;
        }

        setUser(null);
    }

    useEffect(() => {
        auth.onAuthStateChanged((usr) => {
            if (usr) {
                const user = domainUserFromFirebaseUser(usr);
                setUser(user);
                localStorage.setItem('currentUser', JSON.stringify(user));
                return;
            }

            setUser(null);
            localStorage.setItem('currentUser', '');
        });
    }, []);

    return (
        <>
            <Navbar title="Couch Potatoes" user={user}></Navbar>
            <Box
                style={styles}
                padding={`${pageVPaddingInRem}rem ${pageHPaddingInRem}rem ${pageVPaddingInRem}rem ${pageHPaddingInRem}rem`}
            >
                {children}
            </Box>
            ;
        </>
    );
};

export default BasePage;
