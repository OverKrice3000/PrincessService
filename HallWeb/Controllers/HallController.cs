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
    [HttpPost("{attemptId}/reset")]
    public ActionResult ResetHall([FromServices] IHall hall, int attemptId)
    {
        hall.Reset(attemptId);
        return StatusCode(200);
    }

    [HttpPost("{attemptId}/next")]
    [Produces("application/json")]
    public async Task<ActionResult> GetNextContender([FromServices] IPublishEndpoint publishEndpoint,
        [FromServices] IHall hall, int attemptId)
    {
        try
        {
            VisitingContender contender = hall.GetNextContender(attemptId);
            var message = new NextContenderMessage(contender.FullName);
            await publishEndpoint.Publish<NextContenderMessage>(message);
            return StatusCode(200);
        }
        catch (ArgumentException e)
        {
            return StatusCode(400);
        }
    }

    [HttpPost("{attemptId}/select")]
    [Produces("application/json")]
    public ActionResult SelectContender([FromServices] IHall hall, int attemptId)
    {
        try
        {
            int rank = hall.ChooseContender(attemptId);
            return new JsonResult(new SelectContenderResponsePayload(rank.ToString()));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return StatusCode(400);
        }
    }
}