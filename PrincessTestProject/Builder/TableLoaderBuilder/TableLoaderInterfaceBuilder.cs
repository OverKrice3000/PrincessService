namespace PrincessTestProject.Builder.TableLoaderBuilder;

public class TableLoaderInterfaceBuilder
{
    public MSingleColumnLoaderBuilder BuildMTableLoader()
    {
        return new MSingleColumnLoaderBuilder();
    }
}