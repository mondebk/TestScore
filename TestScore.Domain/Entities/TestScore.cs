namespace TestScore.Domain.Entities;

public class TestScore : Entity
{
    public Student Student { get; set; }
    public int Score { get; set; }
    public TestScoreFile File { get; set; }

    public TestScore(Student student, int score, TestScoreFile file)
    {
        Student = student;
        Score = score;
        File = file;
    }
}