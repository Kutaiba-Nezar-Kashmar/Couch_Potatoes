import React, { FC, useEffect } from 'react';
import User from '../../models/user';
import { Flex, Heading, Image, Text } from '@chakra-ui/react';
import { safeConvertDateToString } from '../../util/dateutil';
import { getTextColor } from '../../util/themeutil';
import { Theme } from '../../models/theme';

export interface ProfileInfoProps {
    theme: Theme;
    user: User | null;
}

const ProfileInfo: FC<ProfileInfoProps> = ({ user, theme }) => {
    useEffect(() => {
        console.log(user);
    }, [user]);

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <Flex direction="column" alignItems="flex-start">
            <Heading
                as="h3"
                size={{ base: 'lg', sm: 'md', md: 'lg' }}
                textAlign="start"
                textColor={getTextColor(theme)}
                marginBottom="0.6rem"
            >
                Profile
            </Heading>
            <Image
                rounded="lg"
                width="150px"
                height="150px"
                src={user?.avatarUri!}
            />
            <Heading
                size={{ base: 'lg', lg: 'xl' }}
                marginTop="1rem"
                textColor={getTextColor(theme)}
            >
                {user?.displayName ?? user?.email?.split('@')[0]}
            </Heading>
            <Text
                fontSize={{ base: 'sm', lg: 'md', xl: 'xl' }}
                textColor={getTextColor(theme)}
            >
                {user?.email}
            </Text>
            <Text
                fontSize={{ base: 'xs', lg: 'sm', xl: 'md' }}
                textColor={getTextColor(theme)}
            >
                User since: {safeConvertDateToString(user.createdTimestamp)}
            </Text>
            <Text
                fontSize={{ base: 'xs', lg: 'sm', xl: 'md' }}
                textColor={getTextColor(theme)}
            >
                Last seen online:{' '}
                {safeConvertDateToString(user.lastSignInTimestamp)}
            </Text>
        </Flex>
    );
};

export default ProfileInfo;
