using System.Net;
using CQRS.Domain.Contracts.v1;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers;

public class CoreController : ControllerBase
{
    private readonly INotificationContext _notificationContext;

    public CoreController(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }


    protected IActionResult GetResponse(
        object? response = null,
        HttpStatusCode successCode = HttpStatusCode.OK,
        HttpStatusCode failCode = HttpStatusCode.BadRequest)
    {
        if (_notificationContext.HasNotifications)
            return StatusCode((int)failCode, new { Data = response, _notificationContext.Notifications });

        response = response is null ? null : new { Data = response };

        return StatusCode((int)successCode, response);
    }
}