using PrincessProject.model;

namespace PrincessProject.utils;

public static class Mappers
{
    public static ContenderData[] ContenderToContenderData(Contender[] contenders)
    {
        return contenders.Select(contender => new ContenderData(contender.Name, contender.Surname, contender.Value))
            .ToArray();
    }

    public static string ContenderToContenderName(Contender contender)
    {
        return $"{contender.Name} {contender.Surname}";
    }

    public static VisitingContender ContenderToVisitingContender(Contender contender)
    {
        return new VisitingContender(contender.Name, contender.Surname);
    }
}