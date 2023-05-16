namespace CQRS.Domain.ValueObjects.v1;

public record Email
{
    public Email(string value)
    {
        Value = value.ToUpperInvariant();
    }

    public string Value { get; }
}