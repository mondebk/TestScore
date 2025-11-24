using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.FileProcessor;

namespace TestScoring.Infrastructure.FileHandlers;

public class TestScoreFileProcessor : ITestScoreFileProcessor
{
    public Task<IEnumerable<TestScore>> Process(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var fileInfo = new FileInfo(filePath);

            var lines = File.ReadLines(filePath);

            var testScoreFile = CreateTestScoreFile(fileInfo, lines.Count());

            return Task.FromResult(TestScore.Create(lines, testScoreFile));
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    public Task<IEnumerable<TestScore>> Process(
        string fileContent, 
        string fileName,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static TestScoreFile CreateTestScoreFile(FileInfo fileInfo, int rows)
    {
        return new TestScoreFile(fileInfo.Name, fileInfo.Length, fileInfo.Extension, rows);
    }
}