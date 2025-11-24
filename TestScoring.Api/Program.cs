using Microsoft.EntityFrameworkCore;
using TestScoring.Application.Interfaces;
using TestScoring.Application.Services;
using TestScoring.Domain.Interfaces.FileProcessor;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.FileHandlers;
using TestScoring.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databasePath = DatabasePath.Get();
builder.Services.AddDbContext<TestScoringDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));
        
builder.Services.AddScoped<ITestScoreRepository, TestScoreRepository>();
builder.Services.AddScoped<ITestScoreFileRepository, TestScoreFileRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<ITestScoreRetrievalService, TestScoreRetrievalService>();
builder.Services.AddScoped<ITestScoreFileProcessor, TestScoreFileProcessor>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TestScoringDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
