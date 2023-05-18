import React, { FC, useEffect } from 'react';
import BasePage from '../components/BasePage';
import firebase from 'firebase/compat/app';
import * as firebaseui from 'firebaseui';
import 'firebaseui/dist/firebaseui.css';
import { useNavigate } from 'react-router-dom';
import { auth } from '../firebase';
import { Flex } from '@chakra-ui/react';
import {
    navBarHeightInRem,
    pageVPaddingInRem,
} from '../components/settings/page-settings';

const LoginPage: FC = () => {
    const navigate = useNavigate();
    console.log(auth);

    useEffect(() => {
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
                    requireDisplayName: true,
                },
            ],
            signInSuccessUrl: '/authenticated',
        });
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
            >
                <div id="firebase-auth-container"></div>
            </Flex>
        </BasePage>
    );
};

export default LoginPage;
