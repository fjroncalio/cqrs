using CQRS.Domain.Contracts.v1;
using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Entities.v1;

public class Person : IEntity
{
    private Person(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Person(Name name, Document cpf, Email email, DateTime dateBirth) : 
        this(Guid.NewGuid(), DateTime.Now, DateTime.Now)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    public Person(Guid id, Name name, Document cpf, Email email, DateTime dateBirth, DateTime createdAt) : 
        this(id,createdAt, DateTime.Now)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    //TODO only for easynetq serialize
    public Person() {}


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Name Name { get; set; } = null!;
    public Document Cpf { get; set; } = null!;
    public Email Email { get; set; } = null!;
    public DateTime DateBirth { get; set; }
    public Guid Id { get; set; }
}