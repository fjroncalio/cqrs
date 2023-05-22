using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Entities.v1;

public class Person : Entity
{
    public Person(
        Name name,
        Document cpf,
        Email email,
        DateTime dateBirth)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    public Person(
        Guid id,
        Name name,
        Document cpf,
        Email email,
        DateTime dateBirth,
        DateTime createdAt)
        : base(id, createdAt, DateTime.Now)

    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    public Name Name { get; set; }
    public Document Cpf { get; set; }
    public Email Email { get; set; }
    public DateTime DateBirth { get; set; }
}