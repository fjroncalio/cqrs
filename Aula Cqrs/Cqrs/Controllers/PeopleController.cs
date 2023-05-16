using Cqrs.Domain.Commands.CreatePerson;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;

    public PeopleController(CreatePersonCommandHandler createPersonCommandHandler)
    {
        _createPersonCommandHandler = createPersonCommandHandler;
    }

    [HttpPost(Name = "Insert Person")]
    public async Task<Guid> InsertPeopleAsync(
        CreatePersonCommand createPersonCommand, 
        CancellationToken cancellationToken)
    {
        return await _createPersonCommandHandler
            .HandleAsync(createPersonCommand, cancellationToken);
    }
}