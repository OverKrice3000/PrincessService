using PrincessProject.Data.context;
using PrincessProject.Data.model;

namespace HallWeb.ContenderGeneratorClasses;

public class FromDatabaseContenderGenerator : IContenderGenerator
{
    private int _attemptId = 0;
    private AttemptContext _context;

    public FromDatabaseContenderGenerator(AttemptContext context)
    {
        _context = context;
    }

    public Contender[] Generate(int size = PrincessProject.Data.Constants.DefaultContendersCount)
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