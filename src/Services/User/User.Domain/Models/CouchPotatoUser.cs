namespace User.Domain;

public class CouchPotatoUser
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public List<int> FavoriteMovies { get; set; }= new List<int>();
}

public enum Gender
{
    Male,
    Female,
    Other
}

public static class GenderExtensions
{
    public static string ToString(this Gender gender)
    {
        return gender switch
        {
            Gender.Male => "Male",
            Gender.Female => "Female",
            Gender.Other => "Other"
        };
    }

    public static Gender ToGender(this string gender)
    {
        return gender switch
        {
            "Male" => Gender.Male,
            "M" => Gender.Male,
            "Female" => Gender.Female,
            "F" => Gender.Female,
            "Other" => Gender.Other,
            "O" => Gender.Other,
            _ => throw new ArgumentException($"Failed to parse {gender} into Gender")
        };
    }
}