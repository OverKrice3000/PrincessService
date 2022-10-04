namespace PrincessTestProject.Builder.ContenderContainerBuilder;

public class IContenderContainerBuilder
{
    public MContenderContainerBuilder BuildMContenderContainer()
    {
        return new MContenderContainerBuilder();
    }
}