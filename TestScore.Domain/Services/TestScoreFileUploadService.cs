using TestScore.Domain.Entities;
using TestScore.Domain.Interfaces;

namespace TestScore.Domain.Services;

public class TestScoreFileUploadService
{
    private readonly IStudentScoreRepository _studentScoreRepository;

    public TestScoreFileUploadService(IStudentScoreRepository studentScoreRepository)
    {
        _studentScoreRepository = studentScoreRepository;
    }

    public Task<IEnumerable<StudentScore>> Upload(string filePath, CancellationToken cancellationToken)
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
            
            return Task.FromResult(GetTestScores(lines, testScoreFile));
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    private static TestScoreFile CreateTestScoreFile(FileInfo fileInfo, int rows)
    {
        return new TestScoreFile()
        {
            FileSize = fileInfo.Length,
            FileName = fileInfo.Name,
            FileExtenstion = fileInfo.Extension,
            Rows = rows
        };
    }
    
    private static IEnumerable<Entities.StudentScore> GetTestScores(IEnumerable<string> fileLines,TestScoreFile testScoreFile)
    {
        return fileLines.Skip(1).Select(testScore =>
        {
            var scoreArray =  testScore.Split(',');
            var student = new Student(scoreArray[0], scoreArray[1]);
            return new Entities.StudentScore(student, int.Parse(scoreArray[2]), testScoreFile);
        });
    }
}