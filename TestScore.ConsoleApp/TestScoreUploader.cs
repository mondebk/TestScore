using Microsoft.Extensions.Hosting;
using TestScore.Domain.Interfaces;
using TestScore.Domain.Services;

namespace TestScore.ConsoleApp;

public class TestScoreUploader : IHostedService
{
    private readonly ITestScoreRepository testScoreRepository;
    private readonly TestScoreFileUploadService testScoreFileUploadService;

    public TestScoreUploader(ITestScoreRepository testScoreRepository)
    {
        this.testScoreRepository = testScoreRepository;
        testScoreFileUploadService = new TestScoreFileUploadService(this.testScoreRepository);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Test Score uploader service.");
        Console.WriteLine("Please specify the file path: ");
        var filePath = Console.ReadLine();
        Console.WriteLine($"Processing: {filePath}");
        testScoreFileUploadService.Upload(filePath, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}