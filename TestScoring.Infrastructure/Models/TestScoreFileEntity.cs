using TestScoring.Domain;

namespace TestScoring.Infrastructure.Models;

public class TestScoreFileEntity : Entity
{
    public string Name { get; set; }
    public decimal Size { get; set; }
    public string Extension { get; set; }
    public int Rows { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    public void Update(decimal size, int rows)
    {
        Size = size;
        Rows = rows;
        ModifiedDate = DateTime.UtcNow;
    }
}