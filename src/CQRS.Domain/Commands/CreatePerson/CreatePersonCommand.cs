namespace CQRS.Domain.Commands.CreatePerson;
public class CreatePersonCommand
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime DateBirth { get; set; }
}