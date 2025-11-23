using TestScore.Domain.Entities;
using TestScore.Domain.Interfaces;

namespace TestScore.Domain.Services;

public class TestScoreFileUploadService
{
    private readonly IStudentScoreRepository studentScoreRepository;
    private readonly ITestScoreFileRepository testScoreFileRepository;

    public TestScoreFileUploadService(IStudentScoreRepository studentScoreRepository,
        ITestScoreFileRepository testScoreFileRepository)
    {
        this.studentScoreRepository = studentScoreRepository;
        this.testScoreFileRepository = testScoreFileRepository;
    }

    public async Task<IEnumerable<StudentScore>> Upload(FileStream fileStream, CancellationToken cancellationToken)
    {
        var f = await fileStream.Read(cancellationToken);
    }

    public async Task<IEnumerable<StudentScore>> Upload(string filePath, CancellationToken cancellationToken)
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

            var testScores = GetTestScores(lines);

            await testScoreFileRepository.Add(testScoreFile, cancellationToken);
            await studentScoreRepository.Add(testScores, cancellationToken);

            return testScores;
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    private static TestScoreFile CreateTestScoreFile(FileInfo fileInfo, int rows)
        => new(fileInfo.Name, fileInfo.Length, fileInfo.Extension, rows);

    private static IEnumerable<StudentScore> GetTestScores(IEnumerable<string> fileLines)
    {
        return fileLines.Skip(1).Select(testScore =>
        {
            var scoreArray = testScore.Split(',');
            var student = new Student(scoreArray[0], scoreArray[1]);
            return new StudentScore(student, int.Parse(scoreArray[2]));
        });
    }
}