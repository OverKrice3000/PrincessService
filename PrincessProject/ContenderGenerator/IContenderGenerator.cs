using PrincessProject.model;

namespace PrincessProject.ContenderGenerator;

/*
 * Defines contender generator abstraction, which is able to generate
 * an array of contenders
 */
public interface IContenderGenerator
{
    Contender[] Generate(int size);
}