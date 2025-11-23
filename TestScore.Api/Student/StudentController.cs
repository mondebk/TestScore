using Microsoft.AspNetCore.Mvc;
using TestScore.Domain.Interfaces;
using TestScore.Domain.Services;

namespace TestScore.Api.Student;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private IStudentScoreRepository studentScoreRepository;
    private ScoreService scoreService;

    public StudentController(IStudentScoreRepository studentScoreRepository)
    {
        this.studentScoreRepository = studentScoreRepository;
        this.scoreService = new ScoreService(studentScoreRepository);
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