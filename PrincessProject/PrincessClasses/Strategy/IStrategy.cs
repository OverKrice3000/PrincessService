using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

/*
 * Interface, which describes custom logic for contenders assessment
 */
public interface IStrategy
{
    /*
     * Method, which either approves or rejects a contender by his name
     */
    bool AssessNextContender(ContenderName contender);
}