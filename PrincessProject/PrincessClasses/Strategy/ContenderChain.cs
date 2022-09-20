using PrincessProject.Hall;
using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

public class ContenderChain : IContenderChain
{
    private readonly List<VisitingContender> _contenderChain;
    private readonly IHall _hall;
    private int _size = 0;

    public ContenderChain(IHall hall, int capacity)
    {
        _hall = hall;
        _contenderChain = new List<VisitingContender>(capacity);
    }

    public int Add(VisitingContender visitingContender)
    {
        if (_size == 0)
        {
            _contenderChain.Add(visitingContender);
            _size++;
            return 0;
        }

        bool comparisonResult = _hall.Friend.CompareContenders(visitingContender, _contenderChain[_size / 2])
            .Equals(visitingContender);
        int leftBorder = comparisonResult ? 0 : _size / 2 + 1;
        int rightBorder = comparisonResult ? _size / 2 : _size;
        while (leftBorder != rightBorder)
        {
            comparisonResult = _hall
                .Friend.CompareContenders(visitingContender, _contenderChain[(leftBorder + rightBorder) / 2])
                .Equals(visitingContender);
            rightBorder = comparisonResult ? (leftBorder + rightBorder) / 2 : rightBorder;
            leftBorder = comparisonResult ? leftBorder : (leftBorder + rightBorder) / 2 + 1;
        }

        _contenderChain.Insert(leftBorder, visitingContender);
        _size++;
        return leftBorder;
    }

    public int Size()
    {
        return _size;
    }
}