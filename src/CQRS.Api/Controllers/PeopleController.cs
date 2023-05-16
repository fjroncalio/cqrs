using System.Net;
using CQRS.Domain.Commands.Person.v1.Create;
using CQRS.Domain.Commands.Person.v1.Delete;
using CQRS.Domain.Commands.Person.v1.Update;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Queries.Person.v1.Get;
using CQRS.Domain.Queries.Person.v1.List;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/v1/people")]
public class PeopleController : CoreController
{
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;
    private readonly DeletePersonCommandHandler _deletePersonCommandHandler;
    private readonly GetPersonQueryHandler _getPersonQueryHandler;
    private readonly ListPersonQueryHandler _listPersonQueryHandler;
    private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;

    public PeopleController(INotificationContext notificationContext,
        CreatePersonCommandHandler createPersonCommandHandler,
        DeletePersonCommandHandler deletePersonCommandHandler,
        GetPersonQueryHandler getPersonQueryHandler,
        ListPersonQueryHandler listPersonQueryHandler,
        UpdatePersonCommandHandler updatePersonCommandHandler) : base(notificationContext)
    {
        _createPersonCommandHandler = createPersonCommandHandler;
        _deletePersonCommandHandler = deletePersonCommandHandler;
        _getPersonQueryHandler = getPersonQueryHandler;
        _listPersonQueryHandler = listPersonQueryHandler;
        _updatePersonCommandHandler = updatePersonCommandHandler;
    }


    [HttpGet("{id:guid}", Name = "Get Person By Id")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _getPersonQueryHandler.HandleAsync(new GetPersonQuery(id), cancellationToken);

        return GetResponse(response);
    }

    [HttpGet(Name = "List People")]
    public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] string? cpf,
        CancellationToken cancellationToken)
    {
        var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, cpf), cancellationToken);
        return GetResponse(response);
    }

    [HttpPost(Name = "Insert Person")]
    public async Task<IActionResult> InsertAsync([FromBody] CreatePersonCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _createPersonCommandHandler.HandleAsync(command, cancellationToken);
        return GetResponse(response, HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}", Name = "Update Person")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePersonCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        await _updatePersonCommandHandler.HandleAsync(command, cancellationToken);

        return GetResponse(successCode: HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}", Name = "Delete Person By Id")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _deletePersonCommandHandler.HandleAsync(new DeletePersonCommand(id), cancellationToken);

        return GetResponse(successCode: HttpStatusCode.NoContent);
    }
}