using AutoMapper;
using CQRS.Domain.Commands.CreatePerson;
using CQRS.Domain.Contracts;
using CQRS.Domain.Core;
using CQRS.Domain.Domain;
using System.Net;

namespace CQRS.Domain.Commands.UpdatePerson;
public class UpdatePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var dataBaseEntity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (string.IsNullOrWhiteSpace(dataBaseEntity?.Name))
        {
            AddNotification($"Person with id = {command.Id} does not exist.");
            SetStatusCode(HttpStatusCode.NotFound);
            return Guid.Empty;
        }

        var entity = _mapper.Map<Person>(command);
        entity.CreatedAt = dataBaseEntity.CreatedAt;
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}