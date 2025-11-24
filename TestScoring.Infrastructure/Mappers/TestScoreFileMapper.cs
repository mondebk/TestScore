using TestScoring.Domain.Entities;
using TestScoring.Infrastructure.Models;

namespace TestScoring.Infrastructure.Mappers;

public static class TestScoreFileMapper
{
    public static TestScoreFile ToDomain(TestScoreFileEntity testScoreFileEntity)
    {
        return new TestScoreFile(
            testScoreFileEntity.Name,
            testScoreFileEntity.Size, 
            testScoreFileEntity.Extension, 
            testScoreFileEntity.Rows);
    }

    public static TestScoreFileEntity ToEntity(TestScoreFile testScoreFile)
    {
        return new TestScoreFileEntity
        {
            Name = testScoreFile.FileName,
            Size = testScoreFile.FileSize,
            Extension = testScoreFile.FileExtenstion,
            Rows = testScoreFile.Rows,
            CreatedDate = DateTime.UtcNow,
        };
    }
}