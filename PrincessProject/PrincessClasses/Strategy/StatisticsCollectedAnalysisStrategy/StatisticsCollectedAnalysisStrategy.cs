using PrincessProject.Hall;
using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy.StatisticsCollectedAnalysisStrategy;

public class StatisticsCollectedAnalysisStrategy : IStrategy
{
    private readonly IContenderChain _contenderChain;
    private readonly int _contendersCount;
    private int _targetsPassed;

    private Dictionary<int, Dictionary<int, double>> ChoiceHintsDictionary =
        new Dictionary<int, Dictionary<int, double>>()
        {
            {
                1, new Dictionary<int, double>()
                {
                    { 1, 244547.0 / 1000000 },
                    { 2, 341685.0 / 1000000 },
                    { 3, 236600.0 / 1000000 },
                }
            },
            {
                2, new Dictionary<int, double>()
                {
                    { 2, 339746.0 / 1000000 },
                    { 3, 342937.0 / 1000000 },
                    { 4, 210011.0 / 1000000 },
                }
            },
            {
                3, new Dictionary<int, double>()
                {
                    { 2, 278572.0 / 1000000 },
                    { 3, 381027.0 / 1000000 },
                    { 4, 334471.0 / 1000000 },
                }
            },
            {
                4, new Dictionary<int, double>()
                {
                    { 2, 193936.0 / 1000000 },
                    { 3, 336623.0 / 1000000 },
                    { 4, 389974.0 / 1000000 },
                }
            },
            {
                5, new Dictionary<int, double>()
                {
                    { 3, 245649.0 / 1000000 },
                    { 4, 358910.0 / 1000000 },
                }
            },
            {
                6, new Dictionary<int, double>()
                {
                    { 3, 151844.0 / 1000000 },
                    { 4, 269854.0 / 1000000 },
                }
            },
            {
                7, new Dictionary<int, double>()
                {
                    { 4, 169737.0 / 1000000 },
                }
            },
            {
                8, new Dictionary<int, double>()
                {
                    { 4, 92831.0 / 1000000 },
                }
            },
        };

    private Random? random;

    public StatisticsCollectedAnalysisStrategy(IHall hall)
    {
        _contendersCount = hall.GetTotalCandidates();
        _targetsPassed = 0;
        _contenderChain = new ContenderChain(hall, _contendersCount);
    }

    public bool AssessNextContender(VisitingContender visitingContender)
    {
        if (random == null)
        {
            random = new Random(visitingContender.FullName.);
        }

        int position = _contenderChain.Add(visitingContender);

        if (_contenderChain.Size() < _contendersCount *
            StatisticsCollectedAnalysisStrategyConfig.FirstContendersRejectedPercentage)
        {
            return false;
        }

        if (ChoiceHintsDictionary.ContainsKey(_targetsPassed)
            && ChoiceHintsDictionary[_targetsPassed].ContainsKey(position)
            && ChoiceHintsDictionary[_targetsPassed][position] >= random.NextDouble())
        {
            return true;
        }

        _targetsPassed++;

        return false;
    }
}