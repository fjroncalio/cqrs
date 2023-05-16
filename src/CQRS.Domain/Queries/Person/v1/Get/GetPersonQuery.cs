namespace CQRS.Domain.Queries.Person.v1.Get;

public class GetPersonQuery
{
    public GetPersonQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}