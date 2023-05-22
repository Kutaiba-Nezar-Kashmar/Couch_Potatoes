import React, {FC} from "react";
import {MovieDetailsComponentProps} from "./MovieDetailsBottomInformationbox";
import {Box, Button, Card, CardBody, CardHeader, Heading, Link, Stack, StackDivider, Text} from "@chakra-ui/react";

export const MovieDetailsRightInformationbox: FC<MovieDetailsComponentProps> = ({movie,themeColor}) => {
	return (
		<>

			<Card>
				<CardHeader>
					<Heading size='md'>Quick facts or something</Heading>
				</CardHeader>

				<CardBody>
					<Stack divider={<StackDivider/>} spacing='4'>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Runtime
							</Heading>
							<Text pt='2' fontSize='sm'>
								{movie?.runTime} minutes
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Release Date
							</Heading>
							<Text pt='2' fontSize='sm'>
								{movie?.releaseDate.slice(0, 10)}
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								kid friendly
							</Heading>
							<Text pt='2' fontSize='sm'>
								{movie?.isForKids ? "Yes" : "No"}
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								status
							</Heading>
							<Text pt='2' fontSize='sm'>
								{movie?.status}
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								home page
							</Heading>
							<Link pt='2' fontSize='sm' href={movie?.homepage}
								  color={themeColor}

								  _hover={{color: "blue.700"}}
								  _focus={{outline: "none", boxShadow: "outline"}}
								  _active={{color: "blue.700"}}
								  target="_blank"
								  rel="noopener noreferrer">
								Link to homepage
							</Link>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Budget
							</Heading>
							<Text pt='2' fontSize='sm'>
								${movie?.budget.toLocaleString()}
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Revenue
							</Heading>
							<Text pt='2' fontSize='sm'>
								${movie?.revenue.toLocaleString()}
							</Text>
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								Keywords
							</Heading>

							{movie && movie?.keywords.map((keyword) => (
								<Button margin={0.5} colorScheme={themeColor} size='xs' key={keyword.id}
										className="keyword-box">
									{keyword.name}
								</Button>
							))}
						</Box>
						<Box>
							<Heading size='xs' textTransform='uppercase'>
								spoken languages
							</Heading>
							{movie && movie?.languages.map((language) => (
								<Button margin={0.5} colorScheme={themeColor} size='xs' key={language.code}
										className="keyword-box">
									{language.name}
								</Button>
							))}
						</Box>
					</Stack>
				</CardBody>
			</Card>

		</>
	)
}