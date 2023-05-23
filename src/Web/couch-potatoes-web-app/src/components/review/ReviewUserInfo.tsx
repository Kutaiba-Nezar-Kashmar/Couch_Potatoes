import React, { FC } from 'react';
import User from '../../models/user';
import { Flex, Image, Text } from '@chakra-ui/react';
import { Theme } from '../../models/theme';
import { getTextColor } from '../../util/themeutil';

export interface ReviewUserInfoProps {
    user: User;
    theme: Theme;
}

const ReviewUserInfo: FC<ReviewUserInfoProps> = ({ user, theme }) => {
    return (
        <Flex direction="row" gap={4}>
            <Image
                src={
                    user.avatarUri ??
                    `${process.env['PUBLIC_URL']}/blank-profile.png`
                }
                height={{ base: '100px' }}
                width={{ base: '100px' }}
            />
            <Flex direction="column">
                <Text
                    textColor={getTextColor(theme)}
                    as="b"
                    fontSize={{ base: 'sm', md: 'md', lg: 'lg' }}
                    textTransform="uppercase"
                >
                    {user.displayName ?? user.email?.split('@')[0]}
                </Text>
                <Text textColor={getTextColor(theme)}>
                    Member since:{' '}
                    <Text as="span" textColor={getTextColor(theme)}>
                        {new Date(
                            user.createdTimestamp ?? Date.now.toString()
                        ).toLocaleDateString()}
                    </Text>
                </Text>
                <Text textColor={getTextColor(theme)}>
                    Last seen online:{' '}
                    <Text as="span" textColor={getTextColor(theme)}>
                        {new Date(
                            user.lastSignInTimestamp ?? Date.now.toString()
                        ).toLocaleDateString()}
                    </Text>
                </Text>
            </Flex>
        </Flex>
    );
};

export default ReviewUserInfo;
