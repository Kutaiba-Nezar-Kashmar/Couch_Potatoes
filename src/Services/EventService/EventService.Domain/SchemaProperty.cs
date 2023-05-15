using EventService.Domain.Exceptions;

namespace EventService.Domain;

public class SchemaProperty
{
    public string Title { get; set; }
    public SchemaPropertyType Type { get; set; }
    public IReadOnlyCollection<SchemaProperty> Properties { get; set; }
}

public enum SchemaPropertyType
{
    Object,
    Array,
    String,
    Decimal,
    Int,
    Bool
}

public static class SchemaPropertyTypeExtensions
{
    public static string ToString(this SchemaPropertyType type)
    {
        return type switch
        {
            SchemaPropertyType.Object => "Object",
            SchemaPropertyType.Array => "Array",
            SchemaPropertyType.String => "String",
            SchemaPropertyType.Decimal => "Decimal",
            SchemaPropertyType.Int => "Int",
            SchemaPropertyType.Bool=> "Bool"
        };
    }
    public static SchemaPropertyType ToSchemaPropertyType(this string type)
    {
        return type switch
        {
            "Object" => SchemaPropertyType.Object,
            "Array" => SchemaPropertyType.Array,
            "String" => SchemaPropertyType.String,
            "Decimal" => SchemaPropertyType.Decimal,
            "Int" => SchemaPropertyType.Int,
            "Bool" => SchemaPropertyType.Bool,
            _ => throw new UnknownPropertyTypeException($"Failed to parse type {type} into SchemaPropertyType")
        };
    }
}