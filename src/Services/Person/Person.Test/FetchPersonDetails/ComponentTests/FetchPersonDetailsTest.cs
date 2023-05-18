using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Person.Application.FetchPersonDetails.Repositories;
using Person.Infrastructure.TmdbDtos.PersonDetailsDto;
using Person.Infrastructure.Util.Mappers;
using Person.Test.FetchPersonDetails.Comparers;
using Person.Test.Shared;

namespace Person.Test.FetchPersonDetails.ComponentTests;

[TestFixture]
public class FetchPersonDetailsTest
{
    private Mock<ILogger<FetchPersonDetailsRepository>> _loggerMock;
    private IFetchPersonDetailsRepository _fetchPersonDetailsRepository;
    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private PersonDetailsComparer _personDetailsComparer;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<FetchPersonDetailsRepository>>();
        _personDetailsComparer = new PersonDetailsComparer();
    }

    [Test]
    public async Task FetchPersonDetails_WithPersonID2_ShouldReturnCorrectData()
    {
        // Arrange
        const string file =
            "FetchPersonDetails/ComponentTests/Fakes/PersonDetailsWithId2Response.json";
        var responseString = await File.ReadAllTextAsync(file);

        var factory = TestingUtil.CreateHttpClientFactoryMock(client =>
        {
            client.RegisterGetEndpoint(
                $"https://api.themoviedb.org/3/person/2?api_key={_apiApi}",
                HttpStatusCode.OK, responseString);
            client.SetBaseUri(new Uri("https://api.themoviedb.org/3/"));
        });

        _fetchPersonDetailsRepository =
            new FetchPersonDetailsRepository(_loggerMock.Object,
                factory.Object);
        
        var response =
            JsonSerializer.Deserialize<TmdbPersonDetailsDto>(responseString);
        var mapper = new TmdbPersonDetailsDtoToDomainMapper();
        var expected = mapper.Map(response!);

        // Act
        var result =
            await _fetchPersonDetailsRepository.FetchPersonDetailsById(2);

        // Assert
        Assert.That(_personDetailsComparer.Compare(result, expected), Is.EqualTo(0));
    }
}