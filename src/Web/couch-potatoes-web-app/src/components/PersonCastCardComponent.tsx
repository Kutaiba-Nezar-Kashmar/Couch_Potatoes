import {Card, CardBody, Heading, Image, Stack, Text} from "@chakra-ui/react";
import {getPosterImageUri} from "../services/images";
import React, {FC} from "react";
import MovieRecommendations from "../models/movie-Recommedations";
import CastMember from "../models/cast_member";



export interface PersonCastCardComponentProps {
	castMember: CastMember;
	Background_Temp: string;
	themeColor: string;
	minSize: number;
	maxSize: number;
	isRow?: string
}

export const PersonCastCardComponent: FC<PersonCastCardComponentProps> = ({castMember,Background_Temp,themeColor,minSize,maxSize}) => {
	return (
		<>
			<Card direction={"row"} bg={"white"}
				  variant='outline'
				    maxW='sm' minWidth={minSize} maxWidth={maxSize}>
				{(castMember?.profilePath) ? (<Image
					height={50}
					src={getPosterImageUri(castMember?.profilePath)}
					alt='Profile picture of actor'
					borderRadius='lg'
				/>) : (<Image
					height={50}
					src={Background_Temp}
					alt='No image available of actor'
					borderRadius='lg'
				/>)}
				<CardBody bg={"white"}
					_hover={{cursor: "pointer"}}> {/*TODO: Need onClick event to people page in cardbody*/}


					<Stack mt='3' direction="row">
						<Text size='sm'> {castMember?.name}</Text>
						<Text fontStyle="italic" color={themeColor} fontSize='md'
						>
							{castMember?.character as string}
						</Text>
					</Stack>
				</CardBody>
			</Card>
		</>
	)
}