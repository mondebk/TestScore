using System.Text;

namespace TestScoring.Domain.Entities;

public class TestScore : Entity
{
    public Student Student { get; private set; }
    public int Score { get; private set; }
    public TestScoreFile TestScoreFile { get; private set; }

    public TestScore(Student student, int score)
    {
        Student = student;
        Score = score;
    }

    public TestScore(Student student, int score, TestScoreFile testScoreFile)
    {
        Student = student;
        Score = score;
        TestScoreFile = testScoreFile;
    }

    public static IEnumerable<TestScore> Create(
        IEnumerable<string> fileText,
        TestScoreFile testScoreFile, 
        bool hasHeaderRow = true)
    {
        var rowsToSkip = hasHeaderRow ? 1 : 0;

        return fileText.Skip(rowsToSkip).Select(testScore =>
        {
            var scoreArray = testScore.Split(',');
            var student = new Student(scoreArray[0], scoreArray[1]);
            return new TestScore(student, int.Parse(scoreArray[2]), testScoreFile);
        });
    }

    public static string GetHighestScore(IEnumerable<TestScore> testScores)
    {
        var highestScore = testScores.Max(studentScore => studentScore.Score);

        var studentsWithHighestScore = testScores
            .Where(studentScore => studentScore.Score == highestScore)
            .OrderBy(studentScore => studentScore.Student.FirstName)
            .ThenBy(studentScore => studentScore.Student.LastName)
            .Select(studentScore => (studentScore.Student.FirstName, studentScore.Student.LastName))
            .ToArray();

        var stringBuilder = new StringBuilder();

        for (var i = 0; i < studentsWithHighestScore.Length; i++)
        {
            stringBuilder.Append($"{studentsWithHighestScore[i].FirstName} {studentsWithHighestScore[i].LastName}");

            if (i < studentsWithHighestScore.Length - 1)
                stringBuilder.Append(' ');
        }

        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"Score: {highestScore}");
        return stringBuilder.ToString();
    }
}