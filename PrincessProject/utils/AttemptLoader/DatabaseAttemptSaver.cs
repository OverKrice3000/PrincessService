using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessProject.model;

namespace PrincessProject.utils.AttemptLoader;

public class DatabaseAttemptSaver : IAttemptSaver
{
    private AttemptContext _context;

    public DatabaseAttemptSaver(AttemptContext context)
    {
        _context = context;
    }

    public void Save(Attempt attempt)
    {
        var entity = _context.Add(new AttemptDto()
        {
            ChosenValue = attempt.ChosenContenderValue
        }).Entity;

        _context.SaveChanges();

        for (int i = 0; i < attempt.ContendersCount; i++)
        {
            _context.Add(new AttemptDataDto()
            {
                AttemptId = entity.Id,
                CandidateName = attempt.Contenders[i].Name,
                CandidateSurname = attempt.Contenders[i].Surname,
                CandidateValue = attempt.Contenders[i].Value,
                CandidateOrder = i
            });
        }

        _context.SaveChanges();
    }
}