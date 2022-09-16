using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

public interface IStrategy
{
    bool AssessNextContender(ContenderName contender);
}