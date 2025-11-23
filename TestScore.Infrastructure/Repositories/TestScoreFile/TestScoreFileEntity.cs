using TestScore.Domain;

namespace TestScore.Infrastructure.TestScoreFile;

public class TestScoreFileEntity : Entity
{
    public string Name { get; private set; }
    public decimal Size { get; private set; }
    public string Extension { get; private set; }
    public int Rows { get; private set; }
    public DateTime ModifiedDate { get; private set; }

    public TestScoreFileEntity(string name, decimal size, string extension, int rows)
    {
        Name = name;
        Size = size;
        Extension = extension;
        Rows = rows;
        ModifiedDate = DateTime.UtcNow;
    }
    
    public void Update(decimal size, int rows)
    {
        Size = size;
        Rows = rows;
        ModifiedDate = DateTime.UtcNow;
    }
}