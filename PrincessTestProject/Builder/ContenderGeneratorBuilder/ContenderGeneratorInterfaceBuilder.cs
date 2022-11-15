namespace PrincessTestProject.Builder.ContenderGeneratorBuilder;

public class ContenderGeneratorInterfaceBuilder
{
    public ContenderGeneratorBuilder BuildContenderGenerator()
    {
        return new ContenderGeneratorBuilder();
    }

    public FromDatabaseContenderGeneratorBuilder BuildFromDatabaseContenderGenerator()
    {
        return new FromDatabaseContenderGeneratorBuilder();
    }
}