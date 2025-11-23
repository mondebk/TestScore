using Microsoft.Extensions.Hosting;
using TestScore.Domain.Entities;
using TestScore.Domain.Interfaces;
using TestScore.Domain.Services;

namespace TestScore.ConsoleApp;

public class TestScoreUploader : IHostedService
{
    private readonly IStudentScoreRepository _studentScoreRepository;
    private readonly TestScoreFileUploadService testScoreFileUploadService;

    public TestScoreUploader(IStudentScoreRepository studentScoreRepository)
    {
        this._studentScoreRepository = studentScoreRepository;
        testScoreFileUploadService = new TestScoreFileUploadService(this._studentScoreRepository);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Test Score uploader service.");
        Console.WriteLine("Please specify the file path: ");
        var filePath = Console.ReadLine();
        Console.WriteLine($"Processing: {filePath}");
        var testScores = await testScoreFileUploadService.Upload(filePath, cancellationToken);
        var highestScore = StudentScore.GetHighestScore(testScores);
        Console.WriteLine(highestScore);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}