using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;
using System.Net;
using CQRS.Domain.Entities.v1;

namespace CQRS.Domain.Commands.v1.UpdatePerson;
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
        var dataBaseEntity = await _repository.FindByIdAsync(command.Id, cancellationToken);
        
        if (dataBaseEntity is null)
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