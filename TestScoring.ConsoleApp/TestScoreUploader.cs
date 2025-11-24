using Microsoft.Extensions.Hosting;
using TestScoring.Domain.Entities;
using TestScoring.Infrastructure.FileHandlers;

namespace TestScoring.ConsoleApp;

public class TestScoreUploader : IHostedService
{
    private readonly TestScoreFileProcessor testScoreFileProcessor;

    public TestScoreUploader()
    {
        testScoreFileProcessor = new TestScoreFileProcessor();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Test Score uploader service.");
        Console.WriteLine("Please specify the file path: ");
        var filePath = Console.ReadLine();
        Console.WriteLine($"Processing: {filePath}");

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("File path is empty: ");
        }
        
        var testScores = await testScoreFileProcessor.Process(filePath, cancellationToken);
        var highestScore = TestScore.GetHighestScore(testScores);
        Console.WriteLine(highestScore);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}