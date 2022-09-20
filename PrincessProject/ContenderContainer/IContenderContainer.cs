using PrincessProject.model;

namespace PrincessProject.ContenderContainer;

public interface IContenderContainer
{
    Contender[] Contenders { get; }

    public Contender this[int index] { get; }

    public void Reset(in int size);
}