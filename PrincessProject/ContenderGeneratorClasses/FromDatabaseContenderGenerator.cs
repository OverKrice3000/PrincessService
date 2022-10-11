using PrincessProject.Data.context;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.ContenderGeneratorClasses;

public class FromDatabaseContenderGenerator : IContenderGenerator
{
    private int _attemptId;
    private AttemptContext _context;

    public FromDatabaseContenderGenerator(AttemptContext context, int attemptId)
    {
        _context = context;
        _attemptId = attemptId;
    }

    public Contender[] Generate(int size = Constants.DefaultContendersCount)
    {
        if (_context.Attempts.Where(a => a.AttemptId == _attemptId) is null)
        {
            throw new ArgumentException("No attempt with such id!");
        }

        return _context.Attempts.Where(dto => dto.AttemptId == _attemptId).OrderBy(dto => dto.CandidateOrder)
            .Select(dto => new Contender(dto.CandidateName, dto.CandidateSurname, dto.CandidateValue)).ToArray();
    }

    public void SetAttemptId(int attemptId)
    {
        _attemptId = attemptId;
    }
}