using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetCredits.Exceptions;
using MovieInformation.Application.GetCredits.Repositories;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;
using MovieInformation.Test.Shared;

namespace MovieInformation.Test.GetCredits.ComponentTests;

[TestFixture]
public class GetCreditsRepositoryTest
{
    private Mock<ILogger<GetCreditsRepository>> _loggerMock;
    private IGetCreditsRepository _getCreditsRepository;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<GetCreditsRepository>>();
    }

    [Test]
    public async Task GetMovieCredits_WithMovieId_DoesNotThrowExceptionTest()
    {
        // Arrange
        const int movieId = 550;
        const string file =
            "GetCredits/ComponentTests/Fakes/MovieCreditsResponse.json";
        var responseString = await File.ReadAllTextAsync(file);

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _getCreditsRepository =
            new GetCreditsRepository(factory.Object, _loggerMock.Object);
        var response = JsonDeserializer
            .Deserialize<GetPersonMovieCreditsResponseDto>(responseString);
        // Act

        // Assert
        Assert.DoesNotThrowAsync(async () =>
            await _getCreditsRepository.GetMovieCredits(movieId));
    }

    [Test]
    public async Task
        GetMovieCredits_WithUnsuccessfulStatusCode_ThrowsHttpExceptionTest()
    {
        // Arrange
        const int movieId = 550;
        const string file =
            "GetCredits/ComponentTests/Fakes/MovieCreditsResponse.json";
        var responseString = await File.ReadAllTextAsync(file);

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={_apiApi}",
                HttpStatusCode.InternalServerError, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _getCreditsRepository =
            new GetCreditsRepository(factory.Object, _loggerMock.Object);
        // Act
        var exception = Assert.ThrowsAsync<GetMovieCreditsException>(async () =>
            await _getCreditsRepository.GetMovieCredits(movieId));

        // Assert
        Assert.That(exception.InnerException, Is.TypeOf(typeof(HttpException)));
    }

    [Test]
    public async Task
        GetMovieCredits_WithEmptyResponse_ThrowsHttpResponseExceptionTest()
    {
        // Arrange
        const int movieId = 550;
        var responseString = "";

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        _getCreditsRepository =
            new GetCreditsRepository(factory.Object, _loggerMock.Object);
        // Act
        var exception = Assert.ThrowsAsync<GetMovieCreditsException>(async () =>
            await _getCreditsRepository.GetMovieCredits(movieId));

        // Assert
        Assert.That(exception.InnerException, Is.TypeOf(typeof(HttpResponseException)));
    }
}