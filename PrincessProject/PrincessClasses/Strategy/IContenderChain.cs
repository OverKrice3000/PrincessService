using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

public interface IContenderChain
{
    int Add(ContenderName contender);
    int Size();
}