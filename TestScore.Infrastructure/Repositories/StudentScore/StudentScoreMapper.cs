using TestScore.Domain.Entities;

namespace TestScore.Infrastructure.StudentScore;

public static class StudentScoreMapper
{
    public static Domain.Entities.StudentScore ToDomain(StudentScoreEntity entity)
        => new(new Student(entity.FirstName, entity.LastName), 
            entity.Score);

    public static StudentScoreEntity ToEntity(Domain.Entities.StudentScore entity)
        => new ()
        {
            FirstName = entity.Student.FirstName,
            LastName = entity.Student.LastName,
            Score = entity.Score,
            CreatedDate = DateTime.UtcNow,
        };
}