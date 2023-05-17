using Cqrs.Domain.Commands.CreatePerson;
using Cqrs.Domain.Core;
using Cqrs.Domain.Domain;
using Cqrs.Domain.Queries.ListPerson;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;
    private readonly ListPersonQueryHandler _listPersonQueryHandler;
    public PeopleController(
        CreatePersonCommandHandler createPersonCommandHandler,
        ListPersonQueryHandler listPersonQueryHandler)
    {
        _createPersonCommandHandler = createPersonCommandHandler;
        _listPersonQueryHandler = listPersonQueryHandler;
    }
    
    [HttpPost(Name = "Insert Person")]
    public async Task<IActionResult> InsertPeopleAsync(
        CreatePersonCommand createPersonCommand,
        CancellationToken cancellationToken)
    {
        var result = await _createPersonCommandHandler
            .HandleAsync(createPersonCommand, cancellationToken);


        return GetResponse(_createPersonCommandHandler, result);
    }
    [HttpGet(Name = "List People")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] string? name,
        [FromQuery] string? cpf,
        CancellationToken cancellationToken)
    {
        var result = await _listPersonQueryHandler
            .HandleAsync(
                new ListPersonQuery(name, cpf),
                cancellationToken);

        return GetResponse(_listPersonQueryHandler, result);
    }


    private IActionResult GetResponse<THandler, TResponse>(THandler handler, TResponse response)
        where THandler : BaseHandler
    {
        return StatusCode((int)handler.GetStatusCode(),
            new
            {
                Data = response,
                Notifications = handler.GetNotifications()
            });
    }

    private IActionResult GetResponse<THandler>(THandler handler)
        where THandler : BaseHandler
    {
        return StatusCode((int)handler.GetStatusCode(), 
            new { Notifications = handler.GetNotifications() });
    }
}