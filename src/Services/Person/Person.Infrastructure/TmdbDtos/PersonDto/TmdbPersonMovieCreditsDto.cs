﻿using System.Text.Json.Serialization;

namespace Person.Infrastructure.TmdbDtos.PersonDto;

public record TmdbPersonMovieCreditsDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew
);