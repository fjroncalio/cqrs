namespace CQRS.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommand
{
    public CreatePersonCommand(string? name, string? cpf, string? email, DateTime dateBirth)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    public string? Name { get; }
    public string? Cpf { get; }
    public string? Email { get; }
    public DateTime DateBirth { get; }
}