namespace TestScoring.Infrastructure.Configuration.Database;

public static class DatabasePath
{
    public static string Get()
    {
        var baseDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TestScoring");

        Directory.CreateDirectory(baseDirectory);

        return Path.Combine(baseDirectory, "testscoringapp.db");
    }
}