namespace Person.Infrastructure.Util.Mappers;

public interface IDtoToDomainMapper <TFrom, TTo>
{
    TTo Map(TFrom from);
}