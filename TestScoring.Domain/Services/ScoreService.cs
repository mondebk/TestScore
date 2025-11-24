using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.Repositories;

namespace TestScoring.Domain.Services;

public class ScoreService
{
    private readonly ITestScoreRepository _repository;

    public ScoreService(ITestScoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TestScore>> GetTopScore(CancellationToken cancellationToken = default)
        => await _repository.GetTopScore(cancellationToken);

    public async Task<IEnumerable<TestScore>> Search(string searchTerm,
        CancellationToken cancellationToken = default)
        => await _repository.SearchScore(searchTerm, cancellationToken);
}