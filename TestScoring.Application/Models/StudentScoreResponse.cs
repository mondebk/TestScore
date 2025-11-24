namespace TestScoring.Application.Models;

public class StudentScoreResponse
{
    public IEnumerable<StudentScore> StudentScores { get; private set; }

    public StudentScoreResponse(IEnumerable<StudentScore> studentScores)
    {
        StudentScores = studentScores;
    }
}

public class StudentScore
{
    public string FullName { get; private set; }
    public int Score { get; private set; }

    public StudentScore(string fullName, int score)
    {
        FullName = fullName;
        Score = score;
    }
}