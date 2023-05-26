using Metrics.Domain.Models;
using Metrics.Infrastructure.TmdbDtos.GenreDto;

namespace Metrics.Infrastructure.Util.Mappers;

public class TmdbGenreToDomainMapper : IDtoToDomainMapper<TmdbGenreDto, Genre>
{
    public Genre Map(TmdbGenreDto from)
    {
        return new Genre
        {
            Id = from.Id,
            Name = from.Name
        };
    }
}