using Person.Domain.Models.Person;

namespace Person.Application.FetchPersonDetails.Repositories;

public interface IFetchPersonDetailsRepository
{
    Task<PersonDetails> FetchPersonDetailsById(int personId);
}