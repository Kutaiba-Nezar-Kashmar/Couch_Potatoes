import {Box, Card, CardBody, CardHeader, Heading, Stack, StackDivider, Text} from "@chakra-ui/react";
import React, {FC} from "react";
import MovieCredits from "../../models/movie_credits";


export interface MovieDetailsReviewsComponentProps {

}
export const MovieDetailsReviewsComponent: FC<MovieDetailsReviewsComponentProps> = () => {
	return (
		<>

			<Card>
				<CardHeader>
					<Heading size='md'>Reviews</Heading>
				</CardHeader>

				<CardBody>
					<Stack divider={<StackDivider/>} spacing='4'>

						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Analysis
							</Heading>
							<Text pt='2' fontSize='sm'>
								UwU
							</Text>
						</Box>
					</Stack>
				</CardBody>
			</Card>

		</>
	)
}