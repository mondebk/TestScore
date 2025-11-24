using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestScoring.ConsoleApp;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.Repositories;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TestScorerDbContext>(options =>
            options.UseSqlite("Data Source=testscoreapp.db"));
        
        services.AddSingleton<ITestScoreRepository, TestScoreRepository>();
        services.AddSingleton<ITestScoreFileRepository, TestScoreFileRepository>();
        
        services.AddHostedService<TestScoreUploader>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TestScorerDbContext>();
    db.Database.EnsureCreated();
}

await host.RunAsync();