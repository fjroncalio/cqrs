using AutoMapper;
using CQRS.Domain.Domain;
using CQRS.Domain.Helpers;

namespace CQRS.Domain.Commands.CreatePerson;
public class CreatePersonCommandProfile : Profile
{
    public CreatePersonCommandProfile()
    {
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => input.Cpf.RemoveMaskCpf()))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => input.Name.ToUpper()))
            .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => input.Email.ToUpper()));
    }
}