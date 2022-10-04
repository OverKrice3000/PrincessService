using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrincessProject.Data.model;

public class AttemptDataDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [ForeignKey("AttemptDto")] public int AttemptId { get; set; }

    [Required] public string CandidateName { get; set; }

    [Required] public string CandidateSurname { get; set; }

    [Required] public int CandidateValue { get; set; }

    [Required] public int CandidateOrder { get; set; }

    public AttemptDto AttemptDto { get; set; }
}