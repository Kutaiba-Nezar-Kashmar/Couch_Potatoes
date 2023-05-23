using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Search.Application.MultiSearch.Repositories;
using Search.Infrastructure.Responses;
using Search.Infrastructure.Util.Mappers;
using Search.Test.Shared;
using Search.Infrastructure.Util;

namespace Search.Test.MultiSearch.ComponentTests;

[TestFixture]
public class MultiSearchRepositoryTest
{
    private Mock<ILogger<MultiSearchRepository>> _loggerMock;
    private IMultiSearchRepository _multiSearchRepository;
    private Mock<IMapper> _mapperMock;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<MultiSearchRepository>>();
        _mapperMock = new Mock<IMapper>();
    }

    [Test]
    public async Task MultiSearch_WithNoPersonInResponse_MapMovieListOnly()
    {
        // Arrange
        const string query = "The%20return%20of%20the%20king";
        const string file =
            "MultiSearch/ComponentTests/Fakes/MultiSearchWithNoPersonResponse.json";
        var responseString = await File.ReadAllTextAsync(file);

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/search/multi?query={query}&api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/"));
        });

        _multiSearchRepository = new MultiSearchRepository(_loggerMock.Object,
            factory.Object, _mapperMock.Object);
        var response =
            JsonDeserializer.Deserialize<TmdbMultiSearchResponseDto>(
                responseString);
        var mapper = new TmdbMultiSearchToDomainMapper(_mapperMock.Object);
        var expected = mapper.Map(response!);

        // Act
        var result = await _multiSearchRepository.MultiSearch(query);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.People, Is.Empty);
            Assert.That(result.Movies,
                Has.Count.EqualTo(expected.Movies.Count));
        });
    }

    [Test]
    public async Task MultiSearch_WithNoMovieInResponse_MapPersonListOnly()
    {
        // Arrange
        const string query = "Chris%20Pratt";
        const string file =
            "MultiSearch/ComponentTests/Fakes/MultiSearchWithoutMovieResponse.json";
        var responseString = await File.ReadAllTextAsync(file);

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/search/multi?query={query}&api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/"));
        });

        _multiSearchRepository = new MultiSearchRepository(_loggerMock.Object,
            factory.Object, _mapperMock.Object);
        var response =
            JsonDeserializer.Deserialize<TmdbMultiSearchResponseDto>(
                responseString);
        var mapper = new TmdbMultiSearchToDomainMapper(_mapperMock.Object);
        var expected = mapper.Map(response!);

        // Act
        var result = await _multiSearchRepository.MultiSearch(query);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.Movies, Is.Empty);
            Assert.That(result.People,
                Has.Count.EqualTo(expected.People.Count));
        });
    }
}