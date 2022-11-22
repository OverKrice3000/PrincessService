using PrincessProject.Data.model;

namespace HallWeb.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    public ContenderContainer(int initialSize = PrincessProject.Data.Constants.DefaultContendersCount)
    {
        Container = new Dictionary<int, AttemptContainerContext>();
    }

    public Dictionary<int, AttemptContainerContext> Container { get; private set; }

    public AttemptContainerContext this[int attemptId] => Container[attemptId];

    public Contender FindContenderByName(
        int attemptId,
        VisitingContender visitingContender
    )
    {
        return Array.Find(
                   Container[attemptId].Contenders,
                   contender => contender.FullName.Equals(visitingContender.FullName)
               ) ??
               throw new ArgumentException("No contender with such name!");
    }
}