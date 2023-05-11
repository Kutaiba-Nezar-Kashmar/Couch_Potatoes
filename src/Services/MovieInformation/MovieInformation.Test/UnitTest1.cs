using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetPopularMovies.Repositories;
using MovieInformation.Infrastructure.Exceptions;

namespace MovieInformation.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var mockFactory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            // Close enough
            client.RegisterGetEndpoint("https://api.themoviedb.org/3/movie/popular?api_key=", HttpStatusCode.InsufficientStorage, "{\"statusCode\": 500}");
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/movie/"));
        });

        var loggerMock = new Mock<ILogger>();

        
        IPopularMovieRepository repository = new PopularMovieRepository(mockFactory.Object);
        Assert.ThrowsAsync<HttpException>(async () => await repository.GetPopularMovies(2));
    }
}