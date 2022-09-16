using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.Princess.Strategy;

namespace PrincessProject.PrincessClasses.Strategy;

public class ContenderChain : IContenderChain
{
    private int _size = 0;
    private readonly IHall _hall;
    private readonly List<ContenderName> _contenderChain;

    public ContenderChain(IHall hall, int capacity)
    {
        _hall = hall;
        _contenderChain = new List<ContenderName>(capacity);
    }
    
    public int Add(ContenderName contender)
    {
        if (_size == 0)
        {
            _contenderChain.Add(contender);
            _size++;
            return 0;
        }

        bool comparisonResult = _hall.CompareContenders(contender, _contenderChain[_size / 2]).Equals(contender);
        int leftBorder = comparisonResult ? 0 : _size / 2 + 1;
        int rightBorder = comparisonResult ? _size / 2 : _size;
        while (leftBorder != rightBorder)
        {
            comparisonResult = _hall.CompareContenders(contender, _contenderChain[(leftBorder + rightBorder) / 2])
                .Equals(contender);
            rightBorder = comparisonResult ? (leftBorder + rightBorder) / 2 : rightBorder;
            leftBorder = comparisonResult ? leftBorder : (leftBorder + rightBorder) / 2 + 1;
        }
        _contenderChain.Insert(leftBorder, contender);
        _size++;
        return leftBorder;
    }

    public int Size()
    {
        return _size;
    }
}