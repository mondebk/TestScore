using System.ComponentModel.DataAnnotations;
using TestScore.Domain;

namespace TestScore.Infrastructure.StudentScore;

public class StudentScoreEntity : Entity
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public int Score { get; set; }
}