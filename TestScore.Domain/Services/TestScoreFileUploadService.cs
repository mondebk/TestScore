using TestScore.Domain.Entities;
using TestScore.Domain.Interfaces;

namespace TestScore.Domain.Services;

public class TestScoreFileUploadService
{
    private readonly ITestScoreRepository _testScoreRepository;

     public TestScoreFileUploadService(ITestScoreRepository testScoreRepository)
     {
         _testScoreRepository = testScoreRepository;
     }
    
    public Task Upload(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            
            var testScoreFile = new TestScoreFile()
            {
                FileSize = new FileInfo(filePath).Length,
                FileName = Path.GetFileName(filePath),
            };
            
            var lines = File.ReadLines(filePath);
            
            testScoreFile.Rows = lines.Count();

            Console.WriteLine(lines);
            
            return Task.CompletedTask;
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}