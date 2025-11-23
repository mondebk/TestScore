using TestScore.Domain.Entities;
using TestScore.Domain.Interfaces;

namespace TestScore.Domain.Services;

public class ScoreService
{
    private readonly IStudentScoreRepository _repository;

    public ScoreService(IStudentScoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudentScore>> GetTopScore(CancellationToken cancellationToken = default)
        => await _repository.GetTopScore(cancellationToken);

    public async Task<IEnumerable<StudentScore>> Search(string searchTerm,
        CancellationToken cancellationToken = default)
        => await _repository.SearchScore(searchTerm, cancellationToken);
}