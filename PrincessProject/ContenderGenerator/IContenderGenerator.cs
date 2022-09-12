using PrincessProject.model;

namespace PrincessProject.ContenderGenerator;

public interface IContenderGenerator
{
    Contender[] Generate(int size);
}