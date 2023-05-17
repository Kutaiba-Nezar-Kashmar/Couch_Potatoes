using Google.Cloud.Firestore;
using User.Domain;

namespace User.Application.CreateReviewForMovie.Repository;

public static class FirestoreReviewDtoExtensions
{
    public static Review ToDomainReview(this FirestoreReviewDto dto)
    {
        return new Review()
        {
            UserId = dto.UserId,
            Rating = dto.Rating,
            ReviewText = dto.ReviewText,
            CreationDate = DateTime.Parse(dto.CreationDate),
            Votes = dto.Votes.Select(dtoVote => new Vote()
            {
                UserId = dtoVote.UserId,
                Direction = dtoVote.Direction.ToVoteDirection(),
                Id = Guid.Parse(dtoVote.Id)
            }).ToList(),
            MovieId = dto.MovieId,
            ReviewId = Guid.Parse(dto.ReviewId)
        };
    }

    public static FirestoreReviewDto ToFirestoreReview(this Review review)
    {
        return new FirestoreReviewDto()
        {
            UserId = review.UserId.ToString(),
            ReviewId = review.ReviewId.ToString(),
            MovieId = review.MovieId,
            Rating = review.Rating,
            ReviewText = review.ReviewText,
            CreationDate = review.CreationDate.Date.ToString(),
            Votes = review.Votes.Select(vote => new FirestoreVoteDto()
            {
                UserId = vote.UserId.ToString(),
                Direction = vote.Direction.ToString(),
                Id = vote.Id.ToString()
            }).ToList()
        };
    }
}

[FirestoreData]
public class FirestoreReviewDto
{
    [FirestoreProperty]
    public string ReviewId { get; set; }

    [FirestoreProperty]
    public string UserId { get; set; }

    [FirestoreProperty]
    public int MovieId { get; set; }

    [FirestoreProperty]
    public float Rating { get; set; }

    [FirestoreProperty]
    public string ReviewText { get; set; }

    [FirestoreProperty]
    public string CreationDate { get; set; }

    [FirestoreProperty]
    public IReadOnlyCollection<FirestoreVoteDto> Votes { get; set; } = new List<FirestoreVoteDto>();
}

[FirestoreData]
public class FirestoreVoteDto
{
    [FirestoreProperty]
    public string Direction { get; set; }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string UserId { get; set; }
}