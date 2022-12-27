using HallWeb.Hall;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PrincessProject.Data.model;
using PrincessProject.Data.model.api;
using PrincessProject.Data.model.rabbitmq;

namespace HallWeb.Controllers;

[Route("hall")]
[ApiController]
public class HallController : ControllerBase
{
    private readonly IHall _hall;
    private readonly IPublishEndpoint _publishEndpoint;

    public HallController(IHall hall, IPublishEndpoint publishEndpoint)
    {
        _hall = hall;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("{attemptId}/reset")]
    public ActionResult ResetHall(int attemptId)
    {
        _hall.Reset(attemptId);
        return StatusCode(200);
    }

    [HttpPost("{attemptId}/next")]
    [Produces("application/json")]
    public async Task<ActionResult> GetNextContender(int attemptId)
    {
        try
        {
            VisitingContender contender = _hall.GetNextContender(attemptId);
            var message = new NextContenderMessage(contender.FullName);
            await _publishEndpoint.Publish<NextContenderMessage>(message);
            return StatusCode(200);
        }
        catch (ArgumentException e)
        {
            return StatusCode(400);
        }
    }

    [HttpPost("{attemptId}/select")]
    [Produces("application/json")]
    public ActionResult SelectContender(int attemptId)
    {
        try
        {
            int rank = _hall.ChooseContender(attemptId);
            return new JsonResult(new SelectContenderResponsePayload(rank.ToString()));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return StatusCode(400);
        }
    }
}