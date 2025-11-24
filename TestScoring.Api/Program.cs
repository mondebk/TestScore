using Microsoft.EntityFrameworkCore;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestScorerDbContext>(options =>
    options.UseSqlite("Data Source=testscoreapp.db"));
        
builder.Services.AddScoped<ITestScoreRepository, TestScoreRepository>();
builder.Services.AddScoped<ITestScoreFileRepository, TestScoreFileRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
