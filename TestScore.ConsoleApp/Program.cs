using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestScore.ConsoleApp;
using TestScore.Domain.Interfaces;
using TestScore.Repository;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ITestScoreRepository, TestScoreRepository>();
        services.AddHostedService<TestScoreUploader>();
    })
    .Build();

await host.RunAsync();