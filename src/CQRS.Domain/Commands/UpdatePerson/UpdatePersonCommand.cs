using System.Text.Json.Serialization;

namespace CQRS.Domain.Commands.UpdatePerson;

public class UpdatePersonCommand
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime DateBirth { get; set; }
}