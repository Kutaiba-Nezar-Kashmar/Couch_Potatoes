import { LockIcon, SearchIcon } from '@chakra-ui/icons';
import {
    Avatar,
    Button,
    Card,
    Flex,
    Heading,
    Image,
    Input,
    InputGroup,
    InputLeftElement,
    Stack,
    Text,
    Tooltip,
} from '@chakra-ui/react';
import React, { FC, useState } from 'react';
import User from '../models/user';
import { useNavigate } from 'react-router-dom';
import {
    navBarHeightInRem,
    navBarHPaddingInRem,
    navBarVPaddingInRem,
    pageHPaddingInRem,
} from './settings/page-settings';
import { SearchBar } from './Search/SearchBar';
import { signUserOut } from '../services/user';

interface NavbarProps {
    logoUri?: string;
    title: string;
    user: User | null;
}

const Navbar: FC<NavbarProps> = ({ title, logoUri, user }) => {
    const [displayActions, setDisplayActions] = useState(true);

    const navigate = useNavigate();
    const gotoProfile = () => {
        navigate(`/Couch_Potatoes/profile/${user?.id}`);
    };

    return (
        <nav>
            <Flex
                padding={`${navBarVPaddingInRem}rem ${pageHPaddingInRem}rem`}
                justify="space-between"
                height={`${navBarHeightInRem}rem`}
            >
                <Stack direction="row">
                    {logoUri && <Image src=""></Image>}
                    <Heading
                        fontSize="2xl"
                        cursor="pointer"
                        onClick={() => navigate('/Couch_Potatoes/')}
                        color="white"
                    >
                        {title.toUpperCase()}
                    </Heading>
                </Stack>
                <Flex direction="row">
                    <SearchBar width={300} />
                    {user ? ( // if user then render avatar && the and operator is short for if user exists.
                        <Avatar
                            marginLeft="1rem"
                            name={user.displayName ?? ''}
                            src={user.avatarUri ?? ''}
                            size="sm"
                            cursor="pointer"
                            onClick={gotoProfile}
                        />
                    ) : (
                        <Button
                            marginLeft="2rem"
                            marginTop="0.3rem"
                            size="sm"
                            onClick={() => navigate('/Couch_Potatoes/login')}
                        >
                            Login
                        </Button>
                    )}
                    {user && (
                        <Flex
                            direction="column"
                            justify="space-around"
                            marginLeft="1rem"
                            height="100%"
                        >
                            <Tooltip label="Sign out">
                                <LockIcon
                                    transition="all 0.3s ease-in-out"
                                    _hover={{ color: 'red' }}
                                    cursor="pointer"
                                    color="white"
                                    boxSize={5}
                                    onClick={() =>
                                        signUserOut(
                                            () => navigate('/Couch_Potatoes/'),
                                            (err) => console.error(err)
                                        )
                                    }
                                    marginBottom="1.0rem"
                                />
                            </Tooltip>
                        </Flex>
                    )}
                </Flex>
            </Flex>
        </nav>
    );
};

export default Navbar;
