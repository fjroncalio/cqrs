using System.Collections.Immutable;
using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Helpers.v1;

namespace CQRS.Domain.Queries.Person.v1.List;

public class ListPersonQueryHandler : BaseHandler
{
    private readonly ICacheRepository<List<Entities.v1.Person>> _cacheRepository;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public ListPersonQueryHandler(
        INotificationContext notificationContext,
        IMapper mapper,
        IPersonRepository repository,
        ICacheRepository<List<Entities.v1.Person>> cacheRepository) : base(notificationContext)
    {
        _mapper = mapper;
        _repository = repository;
        _cacheRepository = cacheRepository;
    }

    public async Task<IEnumerable<ListPersonQueryResponse>> HandleAsync(ListPersonQuery command,
        CancellationToken cancellationToken)
    {
        var key = GetKey(new[] { command.Name, command.Cpf });

        var cachePeople = await _cacheRepository.GetAsync(key);

        if (cachePeople != null && cachePeople.Any()) _mapper.Map<IEnumerable<ListPersonQueryResponse>>(cachePeople);

        var people = await _repository.GetAsync(
            person =>
                (command.Name == null || person.Name.Value.Contains(command.Name.ToUpper()))
                && (command.Cpf == null || person.Cpf.Value.Contains(command.Cpf.RemoveMaskCpf()!)),
            cancellationToken
        );

        //TODO add set cache
        return _mapper.Map<IEnumerable<ListPersonQueryResponse>>(people);
    }

    private static string GetKey(IEnumerable<string?> parameters)
    {
        var parametersWithoutEmpties =
            parameters.Where(parameter => !string.IsNullOrWhiteSpace(parameter)).ToImmutableArray();

        return !parametersWithoutEmpties.Any() ? "getAll" : string.Join('|', parametersWithoutEmpties);
    }
}