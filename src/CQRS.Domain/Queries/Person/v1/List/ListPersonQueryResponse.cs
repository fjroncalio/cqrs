namespace CQRS.Domain.Queries.Person.v1.List;

public class ListPersonQueryResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
}