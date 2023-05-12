using AutoMapper;
using CQRS.Domain.Domain;
using CQRS.Domain.Helpers;

namespace CQRS.Domain.Queries.ListPerson;
public class ListPersonQueryProfile : Profile
{
    public ListPersonQueryProfile()
    {
        CreateMap<Person, ListPersonQueryResponse>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => input.Cpf.FormatCpf()));
    }
}