using Microsoft.AspNetCore.Mvc;
using TestScoring.Application.Models.Responses;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Domain.Services;

namespace TestScoring.Api.Student;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private ITestScoreRepository testScoreRepository;
    private ScoreService scoreService;

    public StudentController(ITestScoreRepository testScoreRepository)
    {
        this.testScoreRepository = testScoreRepository;
        this.scoreService = new ScoreService(testScoreRepository);
    }

    [HttpGet("/{studentName}")]
    [ProducesResponseType(typeof(IEnumerable<StudentScoreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<StudentScoreResponse>>> SearchByName(string studentName,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(studentName))
        {
            return BadRequest("Student name cannot empty.");
        }

        var studentResults = scoreService.Search(studentName, cancellationToken);

        return Ok();
    }
}