using PrincessProject.Data.model;

namespace PrincessProject.utils;

public static class Util
{
    public static VisitingContender VisitingContenderFromFullName(string fullName)
    {
        var split = fullName.Split(" ");
        if (split.Length != 2)
        {
            throw new ArgumentException("Bad contender name");
        }

        return new VisitingContender(split[0], split[1]);
    }
}