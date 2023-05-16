using System.Text.Json.Serialization;

namespace CQRS.Domain.Commands.Person.v1.Update;

public class UpdatePersonCommand
{
    public UpdatePersonCommand(string? name, string? cpf, string? email, DateTime dateBirth)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    [JsonIgnore]
    public Guid Id { get; set; }

    public string? Name { get; }
    public string? Cpf { get; }
    public string? Email { get; }
    public DateTime DateBirth { get; }
}