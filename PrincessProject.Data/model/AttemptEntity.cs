using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrincessProject.Data.model;

public class AttemptEntity
{
    public AttemptEntity(
        int attemptId,
        string candidateName,
        string candidateSurname,
        int candidateValue,
        int candidateOrder
    )
    {
        this.AttemptId = attemptId;
        this.CandidateName = candidateName;
        this.CandidateSurname = candidateSurname;
        this.CandidateValue = candidateValue;
        this.CandidateOrder = candidateOrder;
    }

    protected AttemptEntity()
    {
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; private set; }

    public int AttemptId { get; private set; }

    [Required] public string CandidateName { get; private set; }

    [Required] public string CandidateSurname { get; private set; }

    [Required] public int CandidateValue { get; private set; }

    [Required] public int CandidateOrder { get; private set; }
}