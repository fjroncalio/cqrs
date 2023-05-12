using AutoMapper;
using CQRS.Domain.Contracts;
using CQRS.Domain.Core;
using CQRS.Domain.Helpers;

namespace CQRS.Domain.Queries.ListPerson;
public class ListPersonQueryHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public ListPersonQueryHandler(IMapper mapper, IPersonRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ListPersonQueryResponse>> HandleAsync(ListPersonQuery command, CancellationToken cancellationToken)
    {

        var people = await _repository.GetAsync(
            person =>
                    (command.Name == null || person.Name.Contains(command.Name.ToUpper()))
                    && (command.Cpf == null || person.Cpf.Contains(command.Cpf.RemoveMaskCpf())),
                    cancellationToken
                );

        return _mapper.Map<List<ListPersonQueryResponse>>(people);
    }
}