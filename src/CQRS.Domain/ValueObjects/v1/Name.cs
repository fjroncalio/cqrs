namespace CQRS.Domain.ValueObjects.v1;

public record Name
{
    public Name(string value)
    {
        Value = value.ToUpperInvariant();
    }

    public string Value { get; set; }
}
