namespace CQRS.Domain.Commands.v1.DeletePerson;

public class DeletePersonCommand
{
    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}