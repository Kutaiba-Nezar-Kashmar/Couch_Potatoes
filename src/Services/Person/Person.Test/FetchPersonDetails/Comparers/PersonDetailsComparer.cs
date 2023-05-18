using Person.Domain.Models.Person;

namespace Person.Test.FetchPersonDetails.Comparers;

public class PersonDetailsComparer : Comparer<PersonDetails>
{
    public override int Compare(PersonDetails? x, PersonDetails? y)
    {
        if (
            x.IsAdult == y.IsAdult && 
            x.Aliases.Count == y.Aliases.Count &&
            x.Biography == y.Biography &&
            x.Birthday == y.Birthday &&
            x.DeathDay == y.DeathDay &&
            x.Gender == y.Gender &&
            x.Homepage == y.Homepage &&
            x.Id == y.Id &&
            x.ImdbId == y.ImdbId &&
            x.KnownForDepartment == y.KnownForDepartment &&
            x.Name == y.Name &&
            x.PlaceOfBirth == y.PlaceOfBirth &&
            x.Popularity == y.Popularity &&
            x.ProfilePath == y.ProfilePath
        )
        {
            return 0;
        }

        return 1;
    }
}