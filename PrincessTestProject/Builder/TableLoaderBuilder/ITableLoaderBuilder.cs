namespace PrincessTestProject.Builder.TableLoaderBuilder;

public class ITableLoaderBuilder
{
    public MSingleColumnLoaderBuilder BuildMTableLoader()
    {
        return new MSingleColumnLoaderBuilder();
    }
}