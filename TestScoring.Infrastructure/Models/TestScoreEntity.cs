using System.ComponentModel.DataAnnotations;
using TestScoring.Domain;

namespace TestScoring.Infrastructure.Models;

public class TestScoreEntity : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Score { get; set; }
}