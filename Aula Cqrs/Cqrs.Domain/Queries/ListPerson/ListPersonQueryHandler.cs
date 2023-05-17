using AutoMapper;
using Cqrs.Domain.Contracts;
using Cqrs.Domain.Core;

namespace Cqrs.Domain.Queries.ListPerson;

public class ListPersonQueryHandler : BaseHandler
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public ListPersonQueryHandler(
        IPersonRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListPersonQueryResponse>> HandleAsync(ListPersonQuery query, CancellationToken cancellationToken)
    {
        var people = await _repository.GetAsync(
            person =>
                (query.Name == null || person.Name.Contains(query.Name.ToUpper()))
                && (query.Cpf == null || person.Cpf.Contains(query.Cpf)),
            cancellationToken
        );

        return _mapper.Map<IEnumerable<ListPersonQueryResponse>>(people);
    }
}