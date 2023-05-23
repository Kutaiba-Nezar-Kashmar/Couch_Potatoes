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
}

export const PersonCastCardComponent: FC<PersonCastCardComponentProps> = ({castMember,Background_Temp,themeColor,minSize,maxSize}) => {
	return (
		<>
			<Card maxW='sm' minWidth={minSize} maxWidth={maxSize}>
				<CardBody
					_hover={{cursor: "pointer"}}> {/*TODO: Need onClick event to people page in cardbody*/}
					{(castMember?.profilePath) ? (<Image
						src={getPosterImageUri(castMember?.profilePath)}
						alt='Profile picture of actor'
						borderRadius='lg'
					/>) : (<Image minHeight={225}
								  src={Background_Temp}
								  alt='No image available of actor'
								  borderRadius='lg'
					/>)}

					<Stack mt='3' direction="column">
						<Heading size='sm'> {castMember?.name}</Heading>
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