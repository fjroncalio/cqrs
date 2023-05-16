using AutoMapper;
using CQRS.Domain.Helpers.v1;

namespace CQRS.Domain.Queries.Person.v1.Get;

public class GetPersonQueryProfile : Profile
{
    public GetPersonQueryProfile()
    {
        CreateMap<Entities.v1.Person, GetPersonQueryResponse>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => input.Cpf.Value.FormatCpf()))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => input.Name.Value))
            .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => input.Email.Value));
    }
}