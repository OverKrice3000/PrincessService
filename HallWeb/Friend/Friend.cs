using HallWeb.ContenderContainer;
using PrincessProject.Data.model;

namespace HallWeb.Friend;

public class Friend : IFriend
{
    private readonly IContenderContainer _contenderContainer;

    public Friend(IContenderContainer contenderContainer)
    {
        _contenderContainer = contenderContainer;
    }

    public VisitingContender CompareContenders(int attemptId, VisitingContender first, VisitingContender second)
    {
        Contender firstContender = _contenderContainer.FindContenderByName(attemptId, first);
        Contender secondContender = _contenderContainer.FindContenderByName(attemptId, second);
        if (!firstContender.HasVisited || !secondContender.HasVisited)
            throw new ApplicationException("Not so fast baby!");
        return (firstContender.Value < secondContender.Value) ? second : first;
    }
}