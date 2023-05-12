namespace CQRS.Domain.Queries.GetPerson;
public class GetPersonQuery
{
    public GetPersonQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}