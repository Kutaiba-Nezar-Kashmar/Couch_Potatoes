using AutoMapper;
using Search.Domain.Models;
using Search.Infrastructure.ControllerDtos;

namespace Search.Infrastructure.Util.Mappers;

public class MultiSearchToControllerDtoMapper : IDtoToDomainMapper<
    MultiSearchResponse, MultiSearchResponseDto>
{
    private readonly IMapper _mapper;

    public MultiSearchToControllerDtoMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public MultiSearchResponseDto Map(MultiSearchResponse from)
    {
        var response = new MultiSearchResponseDto();
        var people = from.People.Select(p => _mapper.Map<PersonSearchDto>(p))
            .ToList();
        response.People = people;

        var movies = from.Movies.Select(m => _mapper.Map<MovieSearchDto>(m))
            .ToList();
        response.Movies = movies;
        return response;
    }
}