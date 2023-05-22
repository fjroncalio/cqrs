using CQRS.Domain.Commands.v1.CreatePerson;
using CQRS.Domain.Commands.v1.DeletePerson;
using CQRS.Domain.Commands.v1.UpdatePerson;
using CQRS.Domain.Core.v1;
using CQRS.Domain.Queries.v1.GetPerson;
using CQRS.Domain.Queries.v1.ListPerson;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ListPersonQueryHandler _listPersonQueryHandler;
        private readonly GetPersonQueryHandler _getPersonQueryHandler;
        private readonly CreatePersonCommandHandler _createPersonCommandHandler;
        private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;
        private readonly DeletePersonCommandHandler _deletePersonCommandHandler;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(
            ListPersonQueryHandler listPersonQueryHandler,
            GetPersonQueryHandler getPersonQueryHandler,
            CreatePersonCommandHandler createPersonCommandHandler,
            UpdatePersonCommandHandler updatePersonCommandHandler,
            DeletePersonCommandHandler deletePersonCommandHandler,
            ILogger<PeopleController> logger)
        {
            _listPersonQueryHandler = listPersonQueryHandler;
            _getPersonQueryHandler = getPersonQueryHandler;
            _createPersonCommandHandler = createPersonCommandHandler;
            _updatePersonCommandHandler = updatePersonCommandHandler;
            _deletePersonCommandHandler = deletePersonCommandHandler;
            _logger = logger;
        }

        [HttpGet("{id:guid}", Name = "Get Person By Id")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _getPersonQueryHandler.HandleAsync(new GetPersonQuery(id), cancellationToken);
            return GetResponse(_getPersonQueryHandler, response);
        }

        [HttpGet(Name = "List People")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] string? cpf, CancellationToken cancellationToken)
        {
            var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, cpf), cancellationToken);
            return GetResponse(_listPersonQueryHandler, response);
        }

        [HttpPost(Name = "Insert Person")]
        public async Task<IActionResult> InsertAsync([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var response = await _createPersonCommandHandler.HandleAsync(command, cancellationToken);
            return GetResponse(_createPersonCommandHandler, response);
        }

        [HttpPut("{id:guid}", Name = "Update Person")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var response = await _updatePersonCommandHandler.HandleAsync(command, cancellationToken);
            return GetResponse(_updatePersonCommandHandler, response);
        }

        [HttpDelete("{id:guid}", Name = "Delete Person By Id")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _deletePersonCommandHandler.HandleAsync(new DeletePersonCommand(id), cancellationToken);
            return GetResponse(_deletePersonCommandHandler);
        }

        private IActionResult GetResponse<THandler, TResponse>(THandler handler, TResponse response)
            where THandler : BaseHandler
        {
            return StatusCode((int)handler.GetStatusCode(), new { Data = response, Notifications = handler.GetNotifications() });
        }

        private IActionResult GetResponse<THandler>(THandler handler)
            where THandler : BaseHandler
        {
            return StatusCode((int)handler.GetStatusCode(), new { Notifications = handler.GetNotifications() });
        }
    }
}