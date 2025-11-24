using TestScoring.Application.Interfaces;
using TestScoring.Application.Models;
using TestScoring.Domain.Interfaces.Repositories;

namespace TestScoring.Application.Services;

public class TestScoreRetrievalService : ITestScoreRetrievalService
{
    private readonly ITestScoreRepository _testScoreRepository;

    public TestScoreRetrievalService(ITestScoreRepository testScoreRepository)
    {
        _testScoreRepository = testScoreRepository;
    }

    public async Task<StudentScoreResponse?> SearchScoreByStudentName(string studentName,
        CancellationToken cancellationToken)
    {
        var searchResults = await _testScoreRepository.SearchScore(studentName, cancellationToken);

        if (!searchResults.Any())
        {
            return null;
        }

        var studentScores = searchResults.Select(testScore =>
            new StudentScore($"{testScore.Student.FirstName} {testScore.Student.LastName}", testScore.Score));
        
        return new StudentScoreResponse(studentScores);
    }

    public async Task<TopScoreResponse?> GetTopScore(CancellationToken cancellationToken)
    {
        var searchResults = await _testScoreRepository.GetTopScore(cancellationToken);
        
        if (!searchResults.Any())
        {
            return null;
        }
        
        return new TopScoreResponse(
            searchResults.Select(studentScore => $"{studentScore.Student.FirstName} {studentScore.Student.LastName}"),
            searchResults.First().Score);
    }
}