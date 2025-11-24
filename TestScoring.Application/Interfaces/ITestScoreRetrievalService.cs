using TestScoring.Application.Models.Responses;

namespace TestScoring.Application.Interfaces;

public interface ITestScoreRetrievalService
{
    Task<string> GetTopScoreFromFile(string filePath, CancellationToken cancellationToken);
    
    Task<string> GetTopScoresFromFile(string filePath, CancellationToken cancellationToken);
    
    Task<TopScoreResponse> GetTopScoresFromStorage(CancellationToken cancellationToken);
}