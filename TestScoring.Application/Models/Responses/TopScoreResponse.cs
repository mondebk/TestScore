using TestScoring.Domain.Entities;

namespace TestScoring.Application.Models.Responses;

public class TopScoreResponse
{
    public IEnumerable<string> Students { get; private set; }
    public int Score { get; private set;}

    private TopScoreResponse(IEnumerable<string> students, int score)
    {
        Students = students;
        Score = score;
    }

    public static TopScoreResponse FromDomain(IEnumerable<Domain.Entities.TestScore> studentScores)
    {
       return new TopScoreResponse(
           studentScores.Select(studentScore => $"{studentScore.Student.FirstName} {studentScore.Student.LastName}"),
           studentScores.First().Score);
    }
}