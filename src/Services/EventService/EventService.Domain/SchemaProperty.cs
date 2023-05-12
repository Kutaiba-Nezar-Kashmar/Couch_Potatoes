namespace EventService.Domain;

public class SchemaProperty
{
    public string Title{ get; set; }
    public SchemaPropertyType Type { get; set; }
    public IReadOnlyCollection<SchemaProperty> Properties{ get; set; }
}

public enum SchemaPropertyType
{
    Object,
    Array,
    String,
    Decimal,
    Int
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
            SchemaPropertyType.Int => "Int"
        };
    }
}