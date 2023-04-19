import { Box } from '@chakra-ui/react';
import React, { FC, ReactElement, useEffect, useState } from 'react';
import Navbar from './Navbar';
import User from '../models/user';
import {navBarVPaddingInRem, pageHPaddingInRem, pageVPaddingInRem} from "./settings/page-settings";

interface BasePageProps {
    children?: React.ReactNode;
}

const BasePage: FC<BasePageProps> = ({ children }) => {
    const [user, setUser] = useState<User | undefined>(undefined);

    async function getUserIfLoggedIn(): Promise<void> {
        // TODO: (mibui 2023-04-14) Implement this
        setUser({ avatarUri: 'https://bit.ly/dan-abramov', name: 'TEST TEST' });
    }

    useEffect(() => {
        getUserIfLoggedIn();
    }, []);

    return (
        <>
            <Navbar title="Couch Potatoes" user={user}></Navbar>
            <Box padding={`${pageVPaddingInRem}rem ${pageHPaddingInRem}rem ${pageVPaddingInRem}rem ${pageHPaddingInRem}rem`}>{children}</Box>;
        </>
    );
};

export default BasePage;
