using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetMovie.Repositories;
using MovieInformation.Test.Shared;

namespace MovieInformation.Test.GetMovie.ComponentTests;

[TestFixture]
public class GetMovieKeywordsTest
{
    private Mock<ILogger<GetMovieRepository>> _loggerMock;
    private IGetMovieRepository _getMovieRepository;
    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<GetMovieRepository>>();
    }

    [Test]
    public async Task GetKeywords_WithMovieIde_DoesNotThrowExceptions()
    {
        // Arrange
        const int movieId = 550;
        const string file =
            "GetMovie/ComponentTests/Fakes/KeywordsResponse.json";
        var responseString = await File.ReadAllTextAsync(file);
        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _getMovieRepository =
            new GetMovieRepository(factory.Object, _loggerMock.Object);

        // Act

        // Assert
        Assert.DoesNotThrowAsync(async () =>
            await _getMovieRepository.GetMovieKeywords(movieId));
    }
}