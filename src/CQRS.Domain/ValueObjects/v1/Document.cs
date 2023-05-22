using CQRS.Domain.Helpers.v1;

namespace CQRS.Domain.ValueObjects.v1;

public record Document
{
    public Document(string value)
    {
        Value = value.RemoveMaskCpf();
    }

    public string Value { get; set; }
}