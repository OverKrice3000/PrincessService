using PrincessProject.model;

namespace PrincessProject.PrincessClasses;

/*
 * Defines princess abstraction, which is able to choose husband
 */
public interface IPrincess
{
    /*
     * Method, which makes princess assess all the contenders,
     * and optionally choose one
     */
    ContenderName? ChooseHusband();
}