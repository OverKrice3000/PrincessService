using PrincessProject.Data.model;

namespace HallWeb.ContenderContainer;

public class AttemptContainerContext
{
    public Contender[] Contenders;
    public int NextContender;

    public AttemptContainerContext(Contender[] contenders)
    {
        Contenders = contenders;
        NextContender = 0;
    }

    public Contender this[int index] => Contenders[index];
}