using TestScoring.Domain.Entities;
using TestScoring.Infrastructure.Models;

namespace TestScoring.Infrastructure.Mappers;

public static class TestScoreMapper
{
    public static TestScore ToDomain(TestScoreEntity testScoreEntity)
    {
        return new TestScore(
            new Student(testScoreEntity.FirstName, testScoreEntity.LastName),
            testScoreEntity.Score);
    }

    public static TestScoreEntity ToEntity(TestScore testScore)
    {
        return new TestScoreEntity
        {
            FirstName = testScore.Student.FirstName,
            LastName = testScore.Student.LastName,
            Score = testScore.Score,
            CreatedDate = DateTime.UtcNow,
        };
    }
}