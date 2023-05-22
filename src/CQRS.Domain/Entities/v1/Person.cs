using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Entities.v1;

public class Person : Entity
{

    private Person(
        DateTime createdAt, 
        DateTime updatedAt)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    private Person(
        Guid id, 
        DateTime createdAt, 
        DateTime updatedAt) : base(id)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Person(
        Name name, 
        Document cpf, 
        Email email, 
        DateTime dateBirth)
    : this (DateTime.Now, DateTime.Now)
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
        : this(id, createdAt, DateTime.Now)

    {
        Name = name;
        Cpf = cpf;
        Email = email;
        DateBirth = dateBirth;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Name Name { get; set; }
    public Document Cpf { get; set; }
    public Email Email { get; set; }
    public DateTime DateBirth { get; set; }

}