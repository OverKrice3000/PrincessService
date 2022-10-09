using PrincessProject.Data.context;
using PrincessProject.model;

namespace PrincessProject.ContenderGenerator;

public class FromDatabaseContenderGenerator : IContenderGenerator
{
    private int _attemptId;
    private AttemptContext _context;

    public FromDatabaseContenderGenerator(AttemptContext context, int attemptId)
    {
        _context = context;
        _attemptId = attemptId;
    }

    public Contender[] Generate(int size)
    {
        if (_context.AttemptData.Find(_attemptId) is null)
        {
            throw new ArgumentException("No attempt with such id!");
        }

        return _context.AttemptData.Where(dto => dto.AttemptId == _attemptId).OrderBy(dto => dto.CandidateOrder)
            .Select(dto => new Contender(dto.CandidateName, dto.CandidateSurname, dto.CandidateValue)).ToArray();
    }

    public void SetAttemptId(int attemptId)
    {
        _attemptId = attemptId;
    }
}