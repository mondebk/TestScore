using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestScore.ConsoleApp;
using TestScore.Domain.Interfaces;
using TestScore.Infrastructure;
using TestScore.Infrastructure.StudentScore;
using TestScore.Infrastructure.TestScoreFile;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TestScoreDbContext>(options =>
            options.UseSqlite("Data Source=testscoreapp.db"));
        
        services.AddSingleton<IStudentScoreRepository, StudentScoreRepository>();
        services.AddSingleton<ITestScoreFileRepository, TestScoreFileRepository>();
        
        services.AddHostedService<TestScoreUploader>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TestScoreDbContext>();
    db.Database.EnsureCreated();
}

await host.RunAsync();