using AutoMapper;
using CQRS.Domain.Entities.v1;
using CQRS.Domain.Helpers.v1;

namespace CQRS.Domain.Queries.v1.GetPerson;

public class GetPersonQueryProfile : Profile
{
    public GetPersonQueryProfile()
    {
        CreateMap<Person, GetPersonQueryResponse>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => input.Cpf.Value.FormatCpf()))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => input.Name.Value))
            .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => input.Email.Value));
    }
}