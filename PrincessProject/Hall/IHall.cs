using PrincessProject.model;

namespace PrincessProject.Hall;

public interface IHall
{
    int GetTotalCandidates();
    ContenderName GetNextContender();
    ContenderName CompareContenders(ContenderName first, ContenderName second);
}