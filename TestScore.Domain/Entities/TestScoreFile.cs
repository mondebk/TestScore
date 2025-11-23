namespace TestScore.Domain.Entities;

public class TestScoreFile : Entity
{
    public string FileName { get; set; }
    public decimal FileSize { get; set; }
    public string  FileExtenstion { get; set; }
    public int Rows { get; set; } = 0;

}