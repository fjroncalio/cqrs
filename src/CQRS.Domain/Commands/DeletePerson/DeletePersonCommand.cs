namespace CQRS.Domain.Commands.DeletePerson;

public class DeletePersonCommand
{
    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}