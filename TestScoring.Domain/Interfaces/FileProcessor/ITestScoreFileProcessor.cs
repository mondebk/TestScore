using TestScoring.Domain.Entities;

namespace TestScoring.Domain.Interfaces.FileProcessor;

public interface ITestScoreFileProcessor
{
    Task<IEnumerable<TestScore>> Process(string filePath, CancellationToken cancellationToken);

    Task<IEnumerable<TestScore>> Process(
        string fileContent,
        string fileName, 
        string fileExtension, 
        long fileSize,
        CancellationToken cancellationToken);
}