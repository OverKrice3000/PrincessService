using HallWeb.ContenderContainer;
using PrincessProject.Data.model;

namespace PrincessTestProject.Mocks.ContenderContainer;

/// <summary>
/// Mock implementation of IContenderContainer, which generates contenders with unique
/// names and surnames. i-th contender in the container has i value.
/// </summary>
public class MContenderContainer : IContenderContainer
{
    public MContenderContainer(in int size)
    {
        Contenders = _generateContenders(size);
    }

    public Contender[] Contenders { get; private set; }
    public Contender this[int index] => Contenders[index];

    public void Reset(in int size)
    {
        Contenders = _generateContenders(size);
    }

    private Contender[] _generateContenders(in int size)
    {
        Contender[] arr = new Contender[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = new Contender($"Name{i}", $"Surname{i}", i);
        }

        return arr;
    }
}