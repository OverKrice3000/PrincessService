using PrincessProject.model;

namespace PrincessProject.Princess.Strategy;

public interface IContenderChain
{
    int Add(ContenderName contender);
    int Size();
}