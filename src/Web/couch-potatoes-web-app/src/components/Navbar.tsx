import { SearchIcon } from '@chakra-ui/icons';
import {
    Avatar,
    Flex,
    Heading,
    Image,
    Input,
    InputGroup,
    InputLeftElement,
    Stack,
    Text,
} from '@chakra-ui/react';
import React, { FC } from 'react';
import User from '../models/user';
import { useNavigate } from 'react-router-dom';

interface NavbarProps {
    logoUri?: string;
    title: string;
    user?: User;
}

const Navbar: FC<NavbarProps> = ({ title, logoUri, user }) => {
    const navigate = useNavigate();

    return (
        <nav>
            <Flex padding="1rem 8rem" justify="space-between">
                <Stack direction="row">
                    {logoUri && <Image src=""></Image>}
                    <Heading
                        fontSize="2xl"
                        cursor="pointer"
                        onClick={() => navigate('/')}
                    >
                        {title.toUpperCase()}
                    </Heading>
                </Stack>
                <Stack direction="row">
                    <InputGroup>
                        <InputLeftElement
                            pointerEvents="none"
                            children={<SearchIcon color="gray.300" />}
                        />
                        <Input type="text" placeholder="Search" />
                        {user && (
                            <Avatar
                                marginLeft="1rem"
                                name={user.name}
                                src={user.avatarUri}
                                size="sm"
                            />
                        )}
                    </InputGroup>
                </Stack>
            </Flex>
        </nav>
    );
};

export default Navbar;
