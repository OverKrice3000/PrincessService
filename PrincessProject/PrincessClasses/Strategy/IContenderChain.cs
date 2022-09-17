using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

/*
 * Custom queue of contenders
 */
public interface IContenderChain
{
    /*
     * Method, containing custom logic to add contender to chain
     * and return position he has been inserted to
     */
    int Add(ContenderName contender);
    
    /*
     * Method, which returns size of the chain
     */
    int Size();
}