import React, { FC, useEffect, useState } from 'react';
import BasePage from '../components/BasePage';
import { Flex, Grid, GridItem, Spinner, Text } from '@chakra-ui/react';
import ProfileInfo, { ColorScheme } from '../components/profile/ProfileInfo';
import User from '../models/user';
import { auth } from '../firebase';
import { useNavigate } from 'react-router-dom';
import { getAuthenticatedUser, getUserById } from '../services/user';
import BackgroundImageFull from '../components/BackgroundImageFull';
import { navBarHeightInRem } from '../components/settings/page-settings';
import FavoriteMovieDirectory from '../components/profile/FavoriteMovieDirectory';
import { useParams } from 'react-router-dom';

const ProfilePage: FC = () => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const { userId } = useParams();

    const navigate = useNavigate();

    const initProfile = async () => {
        setLoading(true);

        if (!userId) {
            const currentUser = await getAuthenticatedUser();
            if (currentUser) {
                setUser(currentUser);
                setLoading(false);
                return;
            } else {
                navigate('/login');
                return;
            }
        }

        const userToDisplay = await getUserById(userId);
        if (userToDisplay) {
            setUser(userToDisplay);
            setLoading(false);
        } else {
            setUser(null);
            setLoading(false);
        }
    };

    useEffect(() => {
        initProfile();
    }, []);

    if (loading) {
        return (
            <Flex
                width="100%"
                height="100%"
                justifyContent="center"
                alignItems="center"
            >
                <Spinner
                    thickness="4px"
                    speed="0.65s"
                    emptyColor="gray.200"
                    color="blue.500"
                    size="xl"
                />
            </Flex>
        );
    }

    if (!user) {
        <BackgroundImageFull
            imageUri={`${process.env['PUBLIC_URL']}/gradient-bg.jpg`}
        >
            <BasePage>
                <Flex flexDir="row" width="100%" height="100%">
                    <Text> User does not exist</Text>
                </Flex>
            </BasePage>
        </BackgroundImageFull>;
    }

    return (
        <BackgroundImageFull
            imageUri={`${process.env['PUBLIC_URL']}/gradient-bg.jpg`}
        >
            <BasePage>
                <Grid
                    width="100%"
                    templateColumns="repeat(100, 1fr)"
                    height={`calc(100vh - ${navBarHeightInRem}rem)`}
                >
                    <GridItem colSpan={20}>
                        <ProfileInfo
                            colorScheme={ColorScheme.DARK}
                            user={user}
                        />
                    </GridItem>
                    <GridItem colSpan={80}>
                        <FavoriteMovieDirectory
                            movies={[]}
                            colorScheme={ColorScheme.DARK}
                        />
                    </GridItem>
                </Grid>
            </BasePage>
        </BackgroundImageFull>
    );
};

export default ProfilePage;
