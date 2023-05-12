using AutoMapper;
using CQRS.Domain.Domain;
using CQRS.Domain.Helpers;

namespace CQRS.Domain.Queries.GetPerson;
public class GetPersonQueryProfile : Profile
{
    public GetPersonQueryProfile()
    {
        CreateMap<Person, GetPersonQueryResponse>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => input.Cpf.FormatCpf()));
    }
}