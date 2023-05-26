import React, { FC, useEffect, useState } from 'react';
import User from '../../models/user';
import {
    Button,
    Flex,
    FormControl,
    FormErrorMessage,
    FormLabel,
    Heading,
    Image,
    Input,
    Modal,
    ModalBody,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
    Text,
    useDisclosure,
    useToast,
} from '@chakra-ui/react';
import { safeConvertDateToString } from '../../util/dateutil';
import { getTextColor } from '../../util/themeutil';
import { Theme } from '../../models/theme';
import { useQueryClient } from 'react-query';
import { UserCacheKeys, signUserOut } from '../../services/user';
import { CacheKeys } from '../../services/cache-keys';
import { useNavigate } from 'react-router';
import { updateUser, useGetAuthenticatedUser } from '../../services/user';
import { slowClone } from '../../util/clone';
export interface ProfileInfoProps {
    theme: Theme;
    user: User | null;
}

const ProfileInfo: FC<ProfileInfoProps> = ({ user, theme }) => {
    const [displayName, setDisplayName] = useState(user?.displayName);
    const [avatarUri, setAvatarUri] = useState(user?.avatarUri);
    const [authenticatedUser, setAuthenticatedUser] = useState<User | null>(
        null
    );

    const queryClient = useQueryClient();
    const navigate = useNavigate();
    const toast = useToast();

    const {
        isLoading: isLoadingAuthUser,
        isError: isErrorAuthUser,
        data: authUserData,
        error: authUserError,
    } = useGetAuthenticatedUser();

    const { isOpen, onOpen, onClose } = useDisclosure();

    useEffect(() => {
        console.log(user);
        if (!isLoadingAuthUser) {
            setAuthenticatedUser(authUserData as User);
        }
    }, [user, isLoadingAuthUser]);

    if (!user) {
        return <div>Loading...</div>;
    }

    function signOut() {
        signUserOut(
            () => {
                queryClient.invalidateQueries([
                    UserCacheKeys.AUTHENTICATED_USER,
                    UserCacheKeys.GET_USER_WITH_ID + user!.id,
                ]);
                navigate('/Couch_Potatoes/');
            },
            (err) => {
                console.error(err);
            }
        );
    }

    async function updateProfile() {
        if (!user || !authenticatedUser) {
            return;
        }

        if (!avatarUri) {
            setAvatarUri(`${process.env['PUBLIC_URL']}/blank-profile.png`);
        }

        try {
            if (user.id !== authenticatedUser.id) {
                return;
            }
            toast({
                status: 'info',
                title: 'Saving...',
                duration: 4000,
                isClosable: true,
            });

            const updatedUser = await updateUser(
                authenticatedUser.id,
                displayName!,
                avatarUri!
            );

            queryClient.invalidateQueries([
                UserCacheKeys.GET_USER_WITH_ID + authenticatedUser.id,
                UserCacheKeys.AUTHENTICATED_USER,
            ]);

            setAuthenticatedUser(updatedUser);
            toast({
                status: 'success',
                title: 'Profile changes has been saved!',
                duration: 1000,
                isClosable: true,
            });
            onClose();
        } catch (err) {
            toast({
                status: 'error',
                title: 'Error',
                description: 'Failed to save profile changes',
                duration: 4000,
                isClosable: true,
            });
        }
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
            {user && authenticatedUser && authenticatedUser.id === user.id && (
                <Button
                    transition="all 0.3s ease-in-out"
                    _hover={{ backgroundColor: '#222', color: 'white' }}
                    size="sm"
                    marginTop="1rem"
                    width="100%"
                    onClick={() => onOpen()}
                >
                    Edit profile info
                </Button>
            )}
            <Button
                transition="all 0.3s ease-in-out"
                _hover={{ backgroundColor: 'red' }}
                size="sm"
                marginTop="1rem"
                width="100%"
                onClick={() => signOut()}
            >
                Sign out
            </Button>

            <Modal isOpen={isOpen} onClose={onClose}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Update profile</ModalHeader>
                    <ModalBody>
                        <FormControl
                            isInvalid={!authenticatedUser?.displayName}
                        >
                            <FormLabel>Display Name</FormLabel>
                            <FormErrorMessage>
                                Display name must not be empty
                            </FormErrorMessage>
                            <Input
                                value={authenticatedUser?.displayName ?? ''}
                                onChange={(e) => {
                                    const clone = slowClone(authenticatedUser);
                                    if (!clone) {
                                        return;
                                    }
                                    clone!.displayName = e.currentTarget.value;
                                    setAuthenticatedUser(clone);
                                }}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel>Avatar Uri</FormLabel>
                            <Input
                                value={authenticatedUser?.avatarUri ?? ''}
                                onChange={(e) => {
                                    const clone = slowClone(authenticatedUser);
                                    if (!clone) {
                                        return;
                                    }
                                    clone!.avatarUri = e.currentTarget.value;
                                    setAuthenticatedUser(clone);
                                }}
                            />
                        </FormControl>
                    </ModalBody>

                    <ModalFooter>
                        <Button
                            colorScheme="green"
                            onClick={() => updateProfile()}
                        >
                            Save
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </Flex>
    );
};

export default ProfileInfo;
