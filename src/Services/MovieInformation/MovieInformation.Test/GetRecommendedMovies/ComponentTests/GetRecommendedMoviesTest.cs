using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetRecommendedMovies.Repositories;
using MovieInformation.Test.Shared;

namespace MovieInformation.Test.GetRecommendedMovies.ComponentTests;

[TestFixture]
public class GetRecommendedMoviesTest
{
    private Mock<ILogger<GetRecommendedMoviesRepository>> _loggerMock;
    private IGetRecommendedMoviesRepository _getRecommendedMoviesRepository;
    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<GetRecommendedMoviesRepository>>();
    }

    [Test]
    public async Task GetRecommendedMovies_DoesNotThrowExceptions()
    {
        // Arrange
        const int page = 1;
        const int movieId = 550;
        const string file = "GetRecommendedMovies/ComponentTests/Fakes/RecommendedMovieResponse.json";
        
        var responseString = await File.ReadAllTextAsync(file);
        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{movieId}/recommendations?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _getRecommendedMoviesRepository =
            new GetRecommendedMoviesRepository(factory.Object, _loggerMock.Object);

        // Act

        // Assert
        Assert.DoesNotThrowAsync(async () =>
            await _getRecommendedMoviesRepository.GetRecommendedMovies(1, movieId));
    }
}