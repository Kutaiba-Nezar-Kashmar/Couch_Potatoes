using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using MovieInformation.Application.GetMovieCollection.Repositories;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;

namespace MovieInformation.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    // TODO: maybe some tests updates
    // Test cases maybe??

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
        
        IMovieCollectionRepository collectionRepository = new MovieCollectionRepository(mockFactory.Object);
        Assert.ThrowsAsync<HttpException>(async () => await collectionRepository.GetMovieCollection(2, MovieCollection.Popular));
    }
}