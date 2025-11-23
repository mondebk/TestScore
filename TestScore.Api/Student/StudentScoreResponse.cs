namespace TestScore.Api.Student;

public class StudentScoreResponse
{
    public string FullName { get; set; }
    public IEnumerable<int> Results { get; set; }
}