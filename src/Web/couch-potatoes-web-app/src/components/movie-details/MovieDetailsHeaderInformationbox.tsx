import {Box, Stack, StackDivider, Text} from "@chakra-ui/react";
import React, {FC} from "react";
import {MovieDetailsComponentProps} from "./MovieDetailsBottomInformationbox";

export const MovieDetailsHeaderInformationbox : FC<MovieDetailsComponentProps> = ({movie}) => {
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
					<Text color={"white"} fontSize="2xl">
						Rating: {movie?.tmdbScore}/10
						{/* YEAR AND RUNTIME */}

					</Text>
					<Text color={"white"} fontSize="1xl" align="end">
						Vote#: {movie?.tmdbVoteCount}
						{/* TODO: This does not work */}
					</Text>
				</Box>
			</Stack>

		</>
	)
}