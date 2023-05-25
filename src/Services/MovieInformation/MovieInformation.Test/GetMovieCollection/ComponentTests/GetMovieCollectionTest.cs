using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetMovieCollection.Repositories;
using MovieInformation.Test.Shared;

namespace MovieInformation.Test.GetMovieCollection.ComponentTests;

[TestFixture]
public class GetMovieCollectionTest
{
    private Mock<ILogger<MovieCollectionRepository>> _loggerMock;
    private IMovieCollectionRepository _movieCollectionRepository;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<MovieCollectionRepository>>();
    }

    [TestCase("popular")]
    [TestCase("top_rated")]
    [TestCase("upcoming")]
    public async Task GetMovieCollection_WithType_DoesNotThrowExceptions(
        string type)
    {
        // Arrange
        const int page = 1;
        var file = type switch
        {
            "popular" =>
                "GetMovieCollection/ComponentTests/Fakes/PopularMoviesResponse.json",
            "top_rated" =>
                "GetMovieCollection/ComponentTests/Fakes/TopRatedMoviesResponse.json",
            "upcoming" =>
                "GetMovieCollection/ComponentTests/Fakes/UpcomingMoviesResponse.json",
            _ => ""
        };

        var responseString = await File.ReadAllTextAsync(file);
        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{type}?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _movieCollectionRepository =
            new MovieCollectionRepository(factory.Object, _loggerMock.Object);

        // Act

        // Assert
        Assert.DoesNotThrowAsync(async () =>
            await _movieCollectionRepository.GetMovieCollection(1, type));
    }
}