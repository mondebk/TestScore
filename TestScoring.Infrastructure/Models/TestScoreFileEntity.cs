using TestScoring.Domain;

namespace TestScoring.Infrastructure.Models;

public class TestScoreFileEntity : Entity
{
    public string Name { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
    public int Rows { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    public void Update(long size, int rows)
    {
        Size = size;
        Rows = rows;
        ModifiedDate = DateTime.UtcNow;
    }
}