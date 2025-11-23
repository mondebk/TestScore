using TestScore.Domain.Interfaces;

namespace TestScore.Infrastructure.TestScoreFile;

public class TestScoreFileRepository : ITestScoreFileRepository
{
    public Task Add(Domain.Entities.TestScoreFile file, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}