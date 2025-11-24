using Microsoft.Extensions.Hosting;
using TestScoring.Application.Interfaces;
using TestScoring.Domain.Entities;

namespace TestScoring.ConsoleApp;

public class TestScoreUploader : IHostedService
{
    private readonly IFileUploadService _fileUploadService;

    public TestScoreUploader(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Test Score uploader service.");
        Console.WriteLine("Please specify the file path: ");
        var filePath = Console.ReadLine();
        Console.WriteLine($"Processing: {filePath}");
        
        var testScores = await _fileUploadService.UploadFile(filePath, cancellationToken);
        var highestScore = TestScore.GetHighestScore(testScores);
        Console.WriteLine(highestScore);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}