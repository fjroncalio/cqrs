namespace CQRS.Domain.Commands.Person.v1.Delete;

public class DeletePersonCommand
{
    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}