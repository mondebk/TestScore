using Microsoft.AspNetCore.Mvc;
using TestScoring.Api.Score.Models;
using TestScoring.Application.Interfaces;
using TestScoring.Application.Models;

namespace TestScoring.Api.Score;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;
    private readonly ITestScoreRetrievalService _testScoreRetrievalService;

    public ScoreController(IFileUploadService fileUploadService, ITestScoreRetrievalService testScoreRetrievalService)
    {
        _fileUploadService = fileUploadService;
        _testScoreRetrievalService = testScoreRetrievalService;
    }

    [HttpGet("/top")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TopScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TopScoreResponse>> GetTopScore(CancellationToken cancellationToken)
    {
        try
        {
            var topScore = await _testScoreRetrievalService.GetTopScore(cancellationToken);

            if (topScore == null)
            {
                return NoContent();
            }

            return Ok(topScore);
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal error occured");
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(TopScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadTestScoreFile(
        [FromForm] UploadTestScoreFileRequest request,
        CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
        {
            return BadRequest("File is empty");
        }

        try
        {
            var extension = Path.GetExtension(request.File.FileName);

            await using var testScoreFileStream = request.File.OpenReadStream();

            await _fileUploadService.UploadFile(
                testScoreFileStream, 
                request.File.FileName,
                extension,
                request.File.Length,
                cancellationToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("File uploaded successfully");
    }
}