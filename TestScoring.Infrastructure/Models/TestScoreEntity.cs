using System.ComponentModel.DataAnnotations;
using TestScoring.Domain;

namespace TestScoring.Infrastructure.Models;

public class TestScoreEntity : Entity
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public int Score { get; set; }
}