using TestScoring.Application.Models;

namespace TestScoring.Application.Interfaces;

public interface ITestScoreRetrievalService
{
    Task<StudentScoreResponse?> SearchScoreByStudentName(string studentName, CancellationToken cancellationToken);
    
    Task<TopScoreResponse?> GetTopScore(CancellationToken cancellationToken);
}