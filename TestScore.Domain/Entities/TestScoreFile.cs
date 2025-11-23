namespace TestScore.Domain.Entities;

public class TestScoreFile : Entity
{
    public string FileName { get; private set; }
    public decimal FileSize { get; private set; }
    public string  FileExtenstion { get; private set; }
    public int Rows { get; private set; }

    public TestScoreFile(string fileName, decimal fileSize, string fileExtenstion, int rows)
    {
        FileName = fileName;
        FileSize = fileSize;
        FileExtenstion = fileExtenstion;
        Rows = rows;
    }
}