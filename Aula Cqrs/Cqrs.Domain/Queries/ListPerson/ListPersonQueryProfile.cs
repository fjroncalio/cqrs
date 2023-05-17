using AutoMapper;
using Cqrs.Domain.Domain;
using Cqrs.Domain.Helpers;

namespace Cqrs.Domain.Queries.ListPerson;

public class ListPersonQueryProfile : Profile
{
    public ListPersonQueryProfile()
    {
        CreateMap<Person, ListPersonQueryResponse>()
            .ForMember(
                fieldOutput => fieldOutput.Cpf,
                option => 
                    option.MapFrom(input => input.Cpf.FormatCpf()));
    }
}