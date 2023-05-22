namespace CQRS.Domain.Queries.v1.GetPerson;
public class GetPersonQuery
{
    public GetPersonQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}