using PrincessProject.api;
using PrincessProject.Data.model;

namespace PrincessProject.PrincessClasses.Strategy;

public class ContenderChain : IContenderChain
{
    private readonly int _attemptId;
    private readonly List<VisitingContender> _contenderChain;
    private int _size = 0;

    public ContenderChain(int attemptId, int capacity)
    {
        _attemptId = attemptId;
        _contenderChain = new List<VisitingContender>(capacity);
    }

    public async Task<int> Add(VisitingContender visitingContender)
    {
        if (_size == 0)
        {
            _contenderChain.Add(visitingContender);
            _size++;
            return 0;
        }

        bool comparisonResult =
            (await HallApi.CompareContenders(_attemptId, visitingContender, _contenderChain[_size / 2]))
            .Equals(visitingContender);
        int leftBorder = comparisonResult ? 0 : _size / 2 + 1;
        int rightBorder = comparisonResult ? _size / 2 : _size;
        while (leftBorder != rightBorder)
        {
            comparisonResult = (await HallApi.CompareContenders(_attemptId, visitingContender,
                    _contenderChain[(leftBorder + rightBorder) / 2]))
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