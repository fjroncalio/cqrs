using AutoMapper;
using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Commands.Person.v1.Create;

public class CreatePersonCommandProfile : Profile
{
    public CreatePersonCommandProfile()
    {
        CreateMap<CreatePersonCommand, Entities.v1.Person>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => new Document(input.Cpf!)))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => new Name(input.Name!)))
            .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => new Email(input.Email!)));
    }
}