using PrincessProject.Data.model;

namespace PrincessProject.consumers;

public class EventContext
{
    public event Action<int> OnStartAttempt;
    public event Action OnFinishAttempt;
    public event Action<VisitingContender> OnCandidateReceived;

    public void InvokeStartAttempt(int obj)
    {
        OnStartAttempt?.Invoke(obj);
    }

    public void InvokeFinishAttempt()
    {
        OnFinishAttempt?.Invoke();
    }

    public void InvokeCandidateReceived(VisitingContender e)
    {
        OnCandidateReceived?.Invoke(e);
    }
}