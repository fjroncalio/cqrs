using CQRS.Domain.Commands.v1.CreatePerson;
using CQRS.Domain.Commands.v1.DeletePerson;
using CQRS.Domain.Commands.v1.UpdatePerson;
using CQRS.Domain.Queries.v1.GetPerson;
using CQRS.Domain.Queries.v1.ListPerson;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers.v1;

[ApiController]
[Route("api/v1/people")]
public class PeopleController : ControllerBase
{
    private readonly ListPersonQueryHandler _listPersonQueryHandler;
    private readonly GetPersonQueryHandler _getPersonQueryHandler;
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;
    private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;
    private readonly DeletePersonCommandHandler _deletePersonCommandHandler;

    public PeopleController(
        ListPersonQueryHandler listPersonQueryHandler,
        GetPersonQueryHandler getPersonQueryHandler,
        CreatePersonCommandHandler createPersonCommandHandler,
        UpdatePersonCommandHandler updatePersonCommandHandler,
        DeletePersonCommandHandler deletePersonCommandHandler)
    {
        _listPersonQueryHandler = listPersonQueryHandler;
        _getPersonQueryHandler = getPersonQueryHandler;
        _createPersonCommandHandler = createPersonCommandHandler;
        _updatePersonCommandHandler = updatePersonCommandHandler;
        _deletePersonCommandHandler = deletePersonCommandHandler;
    }

    [HttpGet("{id:guid}", Name = "Get Person By Id")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _getPersonQueryHandler.HandleAsync(new GetPersonQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet(Name = "List People")]
    public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] string? cpf, CancellationToken cancellationToken)
    {
        var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, cpf), cancellationToken);
        return Ok(response);
    }

    [HttpPost(Name = "Insert Person")]
    public async Task<IActionResult> InsertAsync([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var response = await _createPersonCommandHandler.HandleAsync(command, cancellationToken);
      
        return Created($"api/v1/people/{response}", new
        {
            id = response, command.Cpf, command.DateBirth, command.Email, command.Name
        });
    }

    [HttpPut("{id:guid}", Name = "Update Person")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await _updatePersonCommandHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:guid}", Name = "Delete Person By Id")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _deletePersonCommandHandler.HandleAsync(new DeletePersonCommand(id), cancellationToken);
        return NoContent();
    }
}