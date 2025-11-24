namespace TestScoring.Application.Models;

public class TopScoreResponse
{
    public IEnumerable<string> Students { get; private set; }
    public int Score { get; private set; }

    public TopScoreResponse(IEnumerable<string> students, int score)
    {
        Students = students;
        Score = score;
    }
}