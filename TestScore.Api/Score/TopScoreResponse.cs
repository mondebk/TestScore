using TestScore.Domain.Entities;

namespace TestScore.Api.Score;

public class TopScoreResponse
{
    public IEnumerable<string> Students { get; }
    public int Score { get; }

    private TopScoreResponse(IEnumerable<string> students, int score)
    {
        Students = students;
        Score = score;
    }

    public static TopScoreResponse FromDomain(IEnumerable<StudentScore> studentScores)
    {
       return new TopScoreResponse(
           studentScores.Select(studentScore => $"{studentScore.Student.FirstName} {studentScore.Student.LastName}"),
           studentScores.First().Score);
    }
}