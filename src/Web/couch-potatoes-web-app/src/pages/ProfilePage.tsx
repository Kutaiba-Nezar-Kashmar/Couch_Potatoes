import React, { FC, useEffect, useState } from 'react';
import BasePage from '../components/BasePage';
import { Flex, Grid, GridItem } from '@chakra-ui/react';
import ProfileInfo, { ColorScheme } from '../components/profile/ProfileInfo';
import User from '../models/user';
import { auth } from '../firebase';
import { useNavigate } from 'react-router-dom';
import { getUser } from '../services/user';
import BackgroundImageFull from '../components/BackgroundImageFull';
import { navBarHeightInRem } from '../components/settings/page-settings';
import FavoriteMovieDirectory from '../components/profile/FavoriteMovieDirectory';

const ProfilePage: FC = () => {
    const [user, setUser] = useState<User | null>(null);

    const navigate = useNavigate();

    const initProfile = async () => {
        const currentUser = await getUser();
        if (currentUser) {
            setUser(currentUser);
        } else {
            navigate('/login');
        }
    };

    useEffect(() => {
        initProfile();
    }, []);

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
