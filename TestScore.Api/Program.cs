using Microsoft.EntityFrameworkCore;
using TestScore.Domain.Interfaces;
using TestScore.Infrastructure;
using TestScore.Infrastructure.StudentScore;
using TestScore.Infrastructure.TestScoreFile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestScoreDbContext>(options =>
    options.UseSqlite("Data Source=testscoreapp.db"));
        
builder.Services.AddScoped<IStudentScoreRepository, StudentScoreRepository>();
builder.Services.AddScoped<ITestScoreFileRepository, TestScoreFileRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
