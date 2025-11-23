using Microsoft.AspNetCore.Mvc;
using TestScore.Domain.Interfaces;
using TestScore.Domain.Services;
using TestScore.Infrastructure.TestScoreFile;

namespace TestScore.Api.Score;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase
{
    private IStudentScoreRepository studentScoreRepository;
    private ITestScoreFileRepository testScoreFileRepository;
    private ScoreService scoreService;
    private TestScoreFileUploadService testScoreFileUploadService;

    public ScoreController(IStudentScoreRepository studentScoreRepository,
        ITestScoreFileRepository testScoreFileRepository)
    {
        this.studentScoreRepository = studentScoreRepository;
        this.testScoreFileRepository = testScoreFileRepository;
        scoreService = new ScoreService(studentScoreRepository);
        testScoreFileUploadService = new TestScoreFileUploadService(studentScoreRepository, testScoreFileRepository);
    }

    [HttpGet("/top")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TopScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TopScoreResponse>> GetTopScore(CancellationToken cancellationToken)
    {
        var topScore = await scoreService.GetTopScore(cancellationToken);

        if (!topScore.Any())
        {
            return NoContent();
        }

        return Ok(TopScoreResponse.FromDomain(topScore));
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(TopScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadTestScoreFile([FromForm] IFormFile scoreFile,
        CancellationToken cancellationToken)
    {
        if (scoreFile.Length == 0)
        {
            return BadRequest("File is empty");
        }

        var filePath = Path.GetTempFileName();

        using (var stream = System.IO.File.Create(filePath))
        {
            await testScoreFileUploadService.Upload(stream, cancellationToken);
        }

        return Task.FromResult<IActionResult>(Ok());
    }
}