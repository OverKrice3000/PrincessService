using PrincessTestProject.Builder.AttemptSaverBuilder;
using PrincessTestProject.Builder.ContenderContainerBuilder;
using PrincessTestProject.Builder.ContenderGeneratorBuilder;
using PrincessTestProject.Builder.ContextBuilder;
using PrincessTestProject.Builder.FriendBuilder;
using PrincessTestProject.Builder.HallBuilder;
using PrincessTestProject.Builder.TableLoaderBuilder;

namespace PrincessTestProject.Builder;

public static class TestBuilder
{
    public static ContenderContainerInterfaceBuilder BuildIContenderContainer()
    {
        return new ContenderContainerInterfaceBuilder();
    }

    public static TableLoaderInterfaceBuilder BuildITableLoader()
    {
        return new TableLoaderInterfaceBuilder();
    }

    public static ContenderGeneratorInterfaceBuilder BuildIContenderGenerator()
    {
        return new ContenderGeneratorInterfaceBuilder();
    }

    public static HallInterfaceBuilder BuildIHall()
    {
        return new HallInterfaceBuilder();
    }

    public static FriendInterfaceBuilder BuildIFriend()
    {
        return new FriendInterfaceBuilder();
    }

    public static DatabaseContextBuilder BuildDatabaseContext()
    {
        return new DatabaseContextBuilder();
    }

    public static AttemptSaverInterfaceBuilder BuildIAttemptSaver()
    {
        return new AttemptSaverInterfaceBuilder();
    }
}