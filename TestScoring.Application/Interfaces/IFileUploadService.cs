using TestScoring.Domain.Entities;

namespace TestScoring.Application.Interfaces;

public interface IFileUploadService
{
    Task<IEnumerable<TestScore>> UploadFile(
        Stream fileStream,
        string fileName,
        string fileExtension,
        long fileSize,
        CancellationToken cancellationToken);

    Task<IEnumerable<TestScore>> UploadFile(string filePath, CancellationToken cancellationToken);
}