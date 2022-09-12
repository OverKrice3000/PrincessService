using PrincessProject.model;

namespace PrincessProject.Princess.Strategy;

public interface IStrategy
{
    bool AssessNextContender(ContenderName contender);
}