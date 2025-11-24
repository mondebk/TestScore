namespace TestScoring.Domain.Entities;

public class TestScoreFile : Entity
{
    public string FileName { get; private set; }
    public long FileSize { get; private set; }
    public string  FileExtenstion { get; private set; }
    public int Rows { get; private set; }

    public TestScoreFile(string fileName, long fileSize, string fileExtenstion, int rows)
    {
        FileName = fileName;
        FileSize = fileSize;
        FileExtenstion = fileExtenstion;
        Rows = rows;
    }
}