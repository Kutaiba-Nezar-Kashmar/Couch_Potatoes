using AutoMapper;
using Search.Domain.Models;
using Search.Infrastructure.Responses;

namespace Search.Infrastructure.Util.Mappers;

public class TmdbMultiSearchToDomainMapper : IDtoToDomainMapper<
    TmdbMultiSearchResponseDto, MultiSearchResponse>
{
    private readonly IMapper _mapper;

    public TmdbMultiSearchToDomainMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public MultiSearchResponse Map(TmdbMultiSearchResponseDto from)
    {
        var response = new MultiSearchResponse();
        var persons = from.Results
            .Where(r => r.MediaType == "person")
            .Select(re => _mapper.Map<PersonSearch>(re))
            .ToList();
        response.People = persons;

        var movies = from.Results
            .Where(r => r.MediaType == "movie")
            .Select(re => _mapper.Map<MovieSearch>(re))
            .ToList();
        response.Movies = movies;
        return response;
    }
}