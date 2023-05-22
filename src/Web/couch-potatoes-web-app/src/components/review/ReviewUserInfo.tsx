import React, { FC } from 'react';
import User from '../../models/user';
import { Flex, Image, Text } from '@chakra-ui/react';

export interface ReviewUserInfoProps {
    user: User;
}

const ReviewUserInfo: FC<ReviewUserInfoProps> = ({ user }) => {
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
                    textColor="white"
                    as="b"
                    fontSize={{ base: 'sm', md: 'md', lg: 'lg' }}
                    textTransform="uppercase"
                >
                    {user.displayName ?? user.email?.split('@')[0]}
                </Text>
                <Text textColor="#D8DEE9">
                    Member since:{' '}
                    <Text as="span" textColor="white">
                        {new Date(
                            user.createdTimestamp ?? Date.now.toString()
                        ).toLocaleDateString()}
                    </Text>
                </Text>
                <Text textColor="#D8DEE9">
                    Last seen online:{' '}
                    <Text as="span" textColor="white">
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
