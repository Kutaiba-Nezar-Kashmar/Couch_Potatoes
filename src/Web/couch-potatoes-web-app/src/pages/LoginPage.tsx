import React, { FC, useEffect } from 'react';
import BasePage from '../components/BasePage';
import firebase from 'firebase/compat/app';
import * as firebaseui from 'firebaseui';
import 'firebaseui/dist/firebaseui.css';
import { useNavigate } from 'react-router-dom';
import { auth } from '../firebase';
import { Flex, Heading } from '@chakra-ui/react';
import {
    navBarHeightInRem,
    pageVPaddingInRem,
} from '../components/settings/page-settings';
import {
    domainUserFromFirebaseUser,
    getAuthenticatedUser,
} from '../services/user';

const LoginPage: FC = () => {
    const navigate = useNavigate();
    const AUTHENTICATED_REDIRECT = '/Couch_Potatoes/authenticated';

    const initLogin = async () => {
        const currentUser = await getAuthenticatedUser();
        if (currentUser) {
            localStorage.setItem('currentUser', JSON.stringify(currentUser));
            navigate(AUTHENTICATED_REDIRECT);
            return;
        }
        const ui =
            firebaseui.auth.AuthUI.getInstance() ||
            new firebaseui.auth.AuthUI(auth);

        ui.start('#firebase-auth-container', {
            signInOptions: [
                {
                    provider: firebase.auth.EmailAuthProvider.PROVIDER_ID,
                    requireDisplayName: true,
                },
                {
                    provider: firebase.auth.GoogleAuthProvider.PROVIDER_ID,
                    scopes: [
                        'https://www.googleapis.com/auth/userinfo.email',
                        'https://www.googleapis.com/auth/userinfo.profile',
                        'https://www.googleapis.com/auth/plus.me',
                    ],
                    customParameters: {
                        prompt: 'consent',
                    },
                    requireDisplayName: true,
                },
            ],
            callbacks: {
                signInSuccessWithAuthResult: (authResult, redirectUrl) => {
                    const res: firebase.auth.UserCredential = authResult;
                    if (res && res.user) {
                        const domainUser = domainUserFromFirebaseUser(res.user);
                        localStorage.setItem(
                            'currentUser',
                            JSON.stringify(currentUser)
                        );

                        navigate(AUTHENTICATED_REDIRECT);
                        return true;
                    }

                    return false;
                },
            },
        });
    };

    useEffect(() => {
        initLogin();
    }, []);

    return (
        <BasePage>
            <Flex
                overflowY="hidden"
                width="100%"
                height={`calc(90vh - ${
                    navBarHeightInRem + pageVPaddingInRem
                }rem)`}
                justify="center"
                alignItems="center"
                flexDirection="column"
            >
                <Heading size={{ base: 'md', lg: 'lg' }}>
                    Couch Potatoes
                </Heading>
                <div id="firebase-auth-container"></div>
            </Flex>
        </BasePage>
    );
};

export default LoginPage;
