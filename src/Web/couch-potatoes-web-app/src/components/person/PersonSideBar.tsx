import React, {FC, ReactElement, useEffect, useState} from 'react';
import {
    Box,
    Image,
    VStack,
    StackDivider,
    Text,
    Heading,
    Grid,
    Flex,
    Spacer,
    Stack,
    Card,
    CardBody, Modal, ModalOverlay, ModalContent, ModalCloseButton, ModalBody
} from "@chakra-ui/react";
import {getPosterImageUri} from "../../services/images";

interface PersonProperties {
    uri?: string;
    alt?: string;
    known?: string;
    gender?: string;
    birthday?: Date;
    placeOfBirth?: string;
    aliases?: string[];
}

const PersonSideBar: FC<PersonProperties> =
    ({
         uri,
         alt,
         known,
         gender,
         birthday,
         placeOfBirth,
         aliases
     }) => {
        const [isOpen, setIsOpen] = useState(false);
        const [expandedImage, setExpandedImage] = useState('');

        const handleImageClick = (imageUrl: string) => {
            setExpandedImage(imageUrl);
            setIsOpen(true);
        };

        const handleModalClose = () => {
            setExpandedImage('');
            setIsOpen(false);
        };


        return (
            <Card backgroundColor="transparent">
                <CardBody>
                    <Image src={uri} alt={alt} borderRadius='lg'
                           onClick={() =>
                               handleImageClick(uri!
                               )
                           }
                    />
                    <Stack divider={<StackDivider/>} spacing='4' paddingTop={4}>
                        <Box>
                            <Heading color="white" as='h4' size='md'>Know For</Heading>
                            <Text color="white">{known}</Text>
                        </Box>
                        <Box>
                            <Heading color="white" as='h4' size='md'>Gender</Heading>
                            <Text color="white">{gender}</Text>
                        </Box>
                        <Box>
                            <Heading color="white" as='h4' size='md'>Birthday</Heading>
                            <Text color="white">{new Date(birthday?? 0).toLocaleDateString()}</Text>
                        </Box>
                        <Box>
                            <Heading color="white" as='h4' size='md'>Place Of Birth</Heading>
                            <Text color="white">{placeOfBirth}</Text>
                        </Box>
                        <Box>
                            <Heading as='h4' size='md' color="white">Aliases</Heading>
                            {aliases?.map(alias => <Text color="white">{alias}</Text>)}
                        </Box>
                    </Stack>
                </CardBody>
                <Modal
                    isOpen={isOpen}
                    onClose={handleModalClose}
                    size="6xl"
                >
                    <ModalOverlay />
                    <ModalContent>
                        <ModalCloseButton />
                        <ModalBody justifyContent="center">
                            <Image
                                src={expandedImage}
                                alt="Expanded Image"
                                mx="auto"
                                maxHeight={750}
                            />
                        </ModalBody>
                    </ModalContent>
                </Modal>
            </Card>

        )
    }
export default PersonSideBar;
