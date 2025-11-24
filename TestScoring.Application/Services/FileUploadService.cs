using TestScoring.Application.Interfaces;
using TestScoring.Domain.Entities;
using TestScoring.Domain.Exceptions;
using TestScoring.Domain.Interfaces.FileProcessor;
using TestScoring.Domain.Interfaces.Repositories;

namespace TestScoring.Application.Services;

public class FileUploadService : IFileUploadService
{
    private readonly ITestScoreFileProcessor _testScoreFileProcessor;
    private readonly ITestScoreFileRepository _testScoreFileRepository;
    private readonly ITestScoreRepository _testScoreRepository;

    public FileUploadService(
        ITestScoreFileProcessor testScoreFileProcessor,
        ITestScoreFileRepository testScoreFileRepository,
        ITestScoreRepository testScoreRepository)
    {
        _testScoreFileProcessor = testScoreFileProcessor;
        _testScoreFileRepository = testScoreFileRepository;
        _testScoreRepository = testScoreRepository;
    }

    public async Task<IEnumerable<TestScore>> UploadFile(
        Stream fileStream,
        string fileName, 
        string fileExtension, 
        long fileSize,
        CancellationToken cancellationToken)
    {
        using var fileStreamReader = new StreamReader(fileStream);

        var testFileContent = await fileStreamReader.ReadToEndAsync(cancellationToken);

        var testScores = await _testScoreFileProcessor.Process(
            testFileContent,
            fileName,
            fileExtension,
            fileSize,
            cancellationToken);

        if (!testScores.Any())
        {
            throw new FileEmptyException("File contains no test scores");
        }

        await SaveTestScores(testScores, cancellationToken);

        return testScores;
    }

    public async Task<IEnumerable<TestScore>> UploadFile(string filePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new FileNotFoundException("File path is empty");
        }

        var testScores = await _testScoreFileProcessor.Process(filePath, cancellationToken);

        if (!testScores.Any())
        {
            throw new FileEmptyException("File contains no test scores");
        }

        await SaveTestScores(testScores, cancellationToken);

        return testScores;
    }

    private async Task SaveTestScores(IEnumerable<TestScore> testScores, CancellationToken cancellationToken)
    {
        await _testScoreFileRepository.Add(testScores.First().TestScoreFile, cancellationToken);

        await _testScoreRepository.Add(testScores, cancellationToken);
    }
}