using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestScoring.Application.Interfaces;
using TestScoring.Application.Services;
using TestScoring.ConsoleApp;
using TestScoring.Domain.Interfaces.FileProcessor;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.FileHandlers;
using TestScoring.Infrastructure.Repositories;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TestScoringDbContext>(options =>
        {
            var dbPath = DatabasePath.Get();
            services.AddDbContext<TestScoringDbContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                options.EnableSensitiveDataLogging(false);
            });
        });

        services.AddSingleton<ITestScoreRepository, TestScoreRepository>();
        services.AddSingleton<ITestScoreFileRepository, TestScoreFileRepository>();
        services.AddSingleton<IFileUploadService, FileUploadService>();
        services.AddSingleton<ITestScoreFileProcessor, TestScoreFileProcessor>();

        services.AddHostedService<TestScoreUploader>();
    })
    .ConfigureLogging(logging => { logging.ClearProviders(); })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<TestScoringDbContext>();
    database.Database.EnsureCreated();
}

await host.RunAsync();