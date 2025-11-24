using Microsoft.AspNetCore.Mvc;
using TestScoring.Application.Interfaces;
using TestScoring.Application.Models.Responses;

namespace TestScoring.Api.Score;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;

    public ScoreController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
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
    public async Task<IActionResult> UploadTestScoreFile(
        [FromForm] IFormFile testScoreFile,
        CancellationToken cancellationToken)
    {
        if (testScoreFile.Length == 0)
        {
            return BadRequest("File is empty");
        }

        try
        {
            await using var testScoreFileStream = testScoreFile.OpenReadStream();
            
            await _fileUploadService.UploadFile(testScoreFileStream, testScoreFile.FileName, cancellationToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("File uploaded successfully");
    }
}