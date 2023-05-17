using MediatR;
using Person.Domain.Models.Person;

namespace Person.Application.FetchPersonDetails;

public record PersonDetailsRequest(int PersonId) : IRequest<PersonDetails>;

public class
    FetchPersonDetailsHandler : IRequestHandler<PersonDetailsRequest,
        PersonDetails>
{
    public Task<PersonDetails> Handle
    (
        PersonDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}