using HallWeb.ContenderContainer;
using HallWeb.utils;
using PrincessProject.Data.model;

namespace HallWeb.Friend;

public class Friend : IFriend
{
    private IContenderContainer _contenderContainer;

    public Friend(IContenderContainer contenderContainer)
    {
        _contenderContainer = contenderContainer;
    }

    public VisitingContender CompareContenders(int attemptId, VisitingContender first, VisitingContender second)
    {
        Contender firstContender = Util.FindContenderByName(_contenderContainer, attemptId, first);
        Contender secondContender = Util.FindContenderByName(_contenderContainer, attemptId, second);
        if (!firstContender.HasVisited || !secondContender.HasVisited)
            throw new ApplicationException("Not so fast baby!");
        return (firstContender.Value < secondContender.Value) ? second : first;
    }
}