using System.ComponentModel.DataAnnotations;

namespace PrincessProject.Data.model;

public class AttemptDataDto
{
    public int id { get; set; }
    public int attempt_id { get; set; }

    [Required] public string candidate_name { get; set; }

    [Required] public string candidate_surname { get; set; }

    [Required] public int candidate_value { get; set; }
}