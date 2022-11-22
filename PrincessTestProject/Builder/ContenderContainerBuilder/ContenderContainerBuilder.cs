using HallWeb.ContenderContainer;

namespace PrincessTestProject.Builder.ContenderContainerBuilder;

public class ContenderContainerBuilder
{
    public IContenderContainer Build()
    {
        return new ContenderContainer();
    }
}