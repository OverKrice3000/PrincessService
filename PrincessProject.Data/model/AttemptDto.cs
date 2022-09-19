using System.ComponentModel.DataAnnotations;

namespace PrincessProject.Data.model;

public class AttemptDto
{
    public int id { get; set; }

    [Required] public int happiness { get; set; }
}