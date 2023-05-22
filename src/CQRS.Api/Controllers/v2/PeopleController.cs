using CQRS.Domain.Queries.v1.ListPerson;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers.v2
{
    [ApiController]
    [Route("api/v2/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ListPersonQueryHandler _listPersonQueryHandler;

        public PeopleController(
            ListPersonQueryHandler listPersonQueryHandler)
        {
            _listPersonQueryHandler = listPersonQueryHandler;
        }


        [HttpGet(Name = "List People v2")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, CancellationToken cancellationToken)
        {
            var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, null), cancellationToken);
            return Ok(response);
        }
       
    }
}