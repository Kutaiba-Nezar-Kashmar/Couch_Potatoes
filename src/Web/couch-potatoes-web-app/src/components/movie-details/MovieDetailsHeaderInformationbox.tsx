import {Box, Image, Stack, StackDivider, Text} from "@chakra-ui/react";
import React, {FC} from "react";
import {MovieDetailsComponentProps} from "./MovieDetailsBottomInformationbox";
import starIcon from "../../assets/iconstar.png";

export const MovieDetailsHeaderInformationbox: FC<MovieDetailsComponentProps> = ({movie}) => {
    return (
        <>

            <Stack direction="row" justify="space-between">
                <Stack direction="column" divider={<StackDivider borderColor='gray.200'/>}>
                    <Stack direction="row">
                        <Box>
                            <Text color={"white"} fontSize="3xl">
                                {movie?.title}
                            </Text>
                        </Box>


                        <Box>
                            <Text color={"gray"} fontSize="3xl">
                                ({movie?.releaseDate.slice(0, 4)})
                            </Text>
                        </Box>

                    </Stack>


                    <Box>
                        <Text fontStyle="italic" color={"white"} fontSize="2l">
                            {movie?.tagLine}
                        </Text>
                    </Box>
                </Stack>


                <Box>
                    <Stack direction="row">
                        <Image src={starIcon} maxHeight={8}/>
                        <Text color={"white"} fontSize="2xl">
                            {movie?.tmdbScore.toFixed(1)}/10
                            {/* YEAR AND RUNTIME */}

                        </Text>

                    </Stack>

                    <Text color={"white"} fontSize="1xl" align="end">
                        Vote#: {movie?.tmdbVoteCount}
                    </Text>
                </Box>
            </Stack>

        </>
    )
}