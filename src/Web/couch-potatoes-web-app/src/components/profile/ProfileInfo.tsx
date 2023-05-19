import React, { FC, useEffect } from 'react';
import User from '../../models/user';
import { Flex, Heading, Image, Text } from '@chakra-ui/react';
import { safeConvertDateToString } from '../../util/dateutil';

export enum ColorScheme {
    DARK,
    LIGHT,
}

export interface ProfileInfoProps {
    colorScheme: ColorScheme;
    user: User | null;
}

const ProfileInfo: FC<ProfileInfoProps> = ({ user, colorScheme }) => {
    const getTextColor = () => {
        return colorScheme === ColorScheme.DARK ? 'white' : 'black';
    };

    useEffect(() => {
        console.log(user);
    }, [user]);

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <Flex direction="column" alignItems="flex-start">
            <Image
                rounded="lg"
                width="150px"
                height="150px"
                src={user?.avatarUri!}
            />
            <Heading
                size={{ base: 'lg', lg: 'xl' }}
                marginTop="1rem"
                textColor={getTextColor()}
            >
                {user?.displayName ?? user?.email?.split('@')[0]}
            </Heading>
            <Text
                fontSize={{ base: 'sm', lg: 'md', xl: 'xl' }}
                textColor={getTextColor()}
            >
                {user?.email}
            </Text>
            <Text
                fontSize={{ base: 'xs', lg: 'sm', xl: 'md' }}
                textColor={getTextColor()}
            >
                User since: {safeConvertDateToString(user.createdTimestamp)}
            </Text>
            <Text
                fontSize={{ base: 'xs', lg: 'sm', xl: 'md' }}
                textColor={getTextColor()}
            >
                Last seen online:{' '}
                {safeConvertDateToString(user.lastSignInTimestamp)}
            </Text>
        </Flex>
    );
};

export default ProfileInfo;
