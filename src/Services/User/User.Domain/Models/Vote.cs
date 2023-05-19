namespace User.Domain;

public class Vote
{
    public VoteDirection Direction { get; set; }
    public Guid Id { get; set; }
    public string UserId { get; set; }
}

public enum VoteDirection
{
    Up,
    Down
}

public static class VoteDirectionExtension
{
    public static string ToString(this VoteDirection direction)
    {
        return direction switch
        {
            VoteDirection.Up => "Up",
            VoteDirection.Down => "Down"
        };
    }

    public static VoteDirection ToVoteDirection(this string direction)
    {
        return direction switch
        {
            "Up" => VoteDirection.Up,
            "Down" => VoteDirection.Down,
            _ => throw new ArgumentException($"Failed to parse {direction} into VoteDirection")
        };
    }
}