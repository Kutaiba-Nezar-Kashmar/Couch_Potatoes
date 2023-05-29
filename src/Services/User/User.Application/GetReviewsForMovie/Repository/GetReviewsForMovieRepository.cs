using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Application.GetReviewsForMovie.Exceptions;
using User.Domain;
using User.Infrastructure;

namespace User.Application.GetReviewsForMovie.Repository;

public class GetReviewsForMovieRepository : IGetReviewsForMovieRepository
{
    private readonly CollectionReference _reference;
    private readonly ILogger _logger;

    public GetReviewsForMovieRepository(ILogger<GetReviewsForMovieRepository> logger)
    {
        _reference = Firestore.Get().Collection("Reviews");
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Review>> Get(int movieId)
    {
        try
        {
            var movieReviewsQuery = _reference.WhereEqualTo("Review.MovieId", movieId);
            var movieReviewsQuerySnapshot = await movieReviewsQuery.GetSnapshotAsync();

            var reviewObjects = movieReviewsQuerySnapshot.Select(docSnapshot => docSnapshot.ToDictionary()["Review"]);

            var reviewsDtos = JsonSerializer.Deserialize<IEnumerable<FirestoreReviewDto>>(
                JsonSerializer.Serialize(reviewObjects,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                )
            );

            if (reviewsDtos is null)
            {
                throw new FailedToRetrieveReviewsException(movieId);
            }

            return reviewsDtos.Select(dto => dto.ToDomainReview()).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to retrieve data from Firestore", e);
            throw;
        }
    }
}