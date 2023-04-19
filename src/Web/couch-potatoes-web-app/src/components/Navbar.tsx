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
import {navBarHeightInRem, navBarHPaddingInRem, navBarVPaddingInRem, pageHPaddingInRem} from "./settings/page-settings";

interface NavbarProps {
    logoUri?: string;
    title: string;
    user?: User;
}

const Navbar: FC<NavbarProps> = ({ title, logoUri, user }) => {
    const navigate = useNavigate();

    return (
        <nav>
            <Flex padding={`${navBarVPaddingInRem}rem ${pageHPaddingInRem}rem`} justify="space-between" height={`${navBarHeightInRem}rem`}>
                <Stack direction="row">
                    {logoUri && <Image src=""></Image>}
                    <Heading
                        fontSize="2xl"
                        cursor="pointer"
                        onClick={() => navigate('/')}
                        color="white"
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
                        <Input type="text" placeholder="Search" backgroundColor="white"/>
                        {user && ( // if user then render avatar && the and operator is short for if user exists.
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
