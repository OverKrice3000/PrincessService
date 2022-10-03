using PrincessTestProject.Builder.ContenderContainerBuilder;
using PrincessTestProject.Builder.ContenderGeneratorBuilder;
using PrincessTestProject.Builder.FriendBuilder;
using PrincessTestProject.Builder.HallBuilder;
using PrincessTestProject.Builder.TableLoaderBuilder;

namespace PrincessTestProject.Builder;

public static class TestBuilder
{
    public static IContenderContainerBuilder BuildIContenderContainer()
    {
        return new IContenderContainerBuilder();
    }

    public static ITableLoaderBuilder BuildITableLoader()
    {
        return new ITableLoaderBuilder();
    }

    public static IContenderGeneratorBuilder BuildIContenderGenerator()
    {
        return new IContenderGeneratorBuilder();
    }

    public static IHallBuilder BuildIHall()
    {
        return new IHallBuilder();
    }

    public static IFriendBuilder BuildIFriend()
    {
        return new IFriendBuilder();
    }
}