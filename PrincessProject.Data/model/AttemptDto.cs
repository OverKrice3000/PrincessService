using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrincessProject.Data.model;

public class AttemptDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required] public int ChosenValue { get; set; }

    public ICollection<AttemptDataDto> AttemptDataDtos { get; set; }
}