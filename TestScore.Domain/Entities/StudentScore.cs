using System.Text;

namespace TestScore.Domain.Entities;

public class StudentScore : Entity
{
    public Student Student { get; private set; }
    public int Score { get; private set; }

    public StudentScore(Student student, int score)
    {
        Student = student;
        Score = score;
    }

    public static string GetHighestScore(IEnumerable<StudentScore> studentScores)
    {
        var highestScore = studentScores.Max(studentScore => studentScore.Score);
        var studentsWithHighestScore = studentScores
            .Where(studentScore => studentScore.Score == highestScore)
            .OrderBy(studentScore => studentScore.Student.FirstName)
            .Select(studentScore => (studentScore.Student.FirstName, studentScore.Student.LastName));

        var stringBuilder = new StringBuilder();
        foreach (var student in studentsWithHighestScore)
        {
            stringBuilder.AppendLine($"{student.FirstName} {student.LastName}");
        }
        stringBuilder.AppendLine(highestScore.ToString());
        return stringBuilder.ToString();
    }
}