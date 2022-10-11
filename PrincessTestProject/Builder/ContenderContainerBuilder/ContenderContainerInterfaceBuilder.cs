namespace PrincessTestProject.Builder.ContenderContainerBuilder;

public class ContenderContainerInterfaceBuilder
{
    public MContenderContainerBuilder BuildMContenderContainer()
    {
        return new MContenderContainerBuilder();
    }

    public ContenderContainerBuilder BuildContenderContainer()
    {
        return new ContenderContainerBuilder();
    }
}