namespace HallWeb.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    public ContenderContainer(int initialSize = PrincessProject.Data.Constants.DefaultContendersCount)
    {
        Container = new Dictionary<int, AttemptContainerContext>();
    }

    public Dictionary<int, AttemptContainerContext> Container { get; private set; }

    public AttemptContainerContext this[int attemptId] => Container[attemptId];
}