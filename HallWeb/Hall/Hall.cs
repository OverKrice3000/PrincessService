using HallWeb.ContenderContainer;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.utils;
using HallWeb.utils.ResultSaver;
using PrincessProject.Data.model;
using PrincessProject.Data.model.data;

namespace HallWeb.Hall;

public class Hall : IHall
{
    private readonly IContenderContainer _contenderContainer;
    private readonly int _size;
    private FromDatabaseContenderGenerator _generator;
    private IResultSaver _resultSaver;

    public Hall(
        IResultSaver resultSaver,
        FromDatabaseContenderGenerator generator,
        IContenderContainer contenderContainer,
        int size = PrincessProject.Data.Constants.DefaultContendersCount
    )
    {
        _size = size;
        _contenderContainer = contenderContainer;
        _resultSaver = resultSaver;
        _generator = generator;
    }

    public int GetTotalCandidates()
    {
        return _size;
    }

    public VisitingContender GetNextContender(int attemptId)
    {
        if (!_contenderContainer.Container.ContainsKey(attemptId))
        {
            _generator.SetAttemptId(attemptId);
            _contenderContainer.Container[attemptId] = new AttemptContainerContext(_generator.Generate());
        }

        if (_contenderContainer[attemptId].Contenders.Length <=
            _contenderContainer[attemptId].NextContender)
        {
            throw new ArgumentException("No more contenders!");
        }

        Contender nextContender = _contenderContainer[attemptId][_contenderContainer[attemptId].NextContender++];
        if (Constants.DebugMode)
        {
            Console.WriteLine("NEXT CONTENDER IS:");
            Console.WriteLine(nextContender.Value);
        }

        nextContender.SetHasVisited();
        return Mappers.ContenderToVisitingContender(nextContender);
    }

    public void Reset(int attemptId)
    {
        if (!_contenderContainer.Container.ContainsKey(attemptId))
        {
            _generator.SetAttemptId(attemptId);
            _contenderContainer.Container[attemptId] = new AttemptContainerContext(_generator.Generate());
        }
        else
        {
            _contenderContainer.Container[attemptId].NextContender = 0;
        }
    }

    public int ChooseContender(int attemptId)
    {
        if (!_contenderContainer.Container.ContainsKey(attemptId) ||
            _contenderContainer.Container[attemptId].NextContender == 0)
        {
            throw new ArgumentException("No contender was with princess!");
        }

        Contender contender = _contenderContainer[attemptId][_contenderContainer[attemptId].NextContender - 1];

        _resultSaver.Save(new Result(
            PrincessProject.Data.Constants.DefaultContendersCount,
            Mappers.ContenderToContenderData(_contenderContainer[attemptId].Contenders),
            contender.Value
        ));

        return contender.Value;
    }

    public void SetAttemptSaver(IResultSaver resultSaver)
    {
        _resultSaver = resultSaver;
    }
}