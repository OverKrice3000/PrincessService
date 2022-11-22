using HallWeb.Friend;
using HallWeb.utils;
using Microsoft.AspNetCore.Mvc;
using PrincessProject.Data.model;
using PrincessProject.Data.model.api;

namespace HallWeb.Controllers;

[Route("friend")]
[ApiController]
public class FriendController : ControllerBase
{
    [HttpPost("{attemptId}/compare")]
    [Produces("application/json")]
    public ActionResult CompareContenders([FromServices] IFriend friend, int attemptId,
        [FromBody] CompareContendersRequestPayload payload)
    {
        try
        {
            VisitingContender firstContender = Util.VisitingContenderFromFullName(payload.first);
            VisitingContender secondContender = Util.VisitingContenderFromFullName(payload.second);
            return new JsonResult(new CompareContendersResponsePayload(friend
                .CompareContenders(attemptId, firstContender, secondContender).FullName));
        }
        catch (ArgumentException e)
        {
            return StatusCode(400);
        }
    }
}