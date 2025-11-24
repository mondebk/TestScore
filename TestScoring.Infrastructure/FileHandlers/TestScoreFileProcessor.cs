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

            var testScoreFile = new TestScoreFile(
                fileInfo.Name,
                fileInfo.Length,
                fileInfo.Extension,
                lines.Count());

            return Task.FromResult(TestScore.Create(lines, testScoreFile));
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TestScore>> Process(
        string fileContent,
        string fileName,
        string fileExtension,
        long fileSize,
        CancellationToken cancellationToken)
    {
        try
        {
            var lines = fileContent.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries);

            var testScoreFile = new TestScoreFile(fileName, fileSize, fileExtension, lines.Length);

            return TestScore.Create(lines, testScoreFile);
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}