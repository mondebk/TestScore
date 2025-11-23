using Microsoft.AspNetCore.Mvc;

namespace TestScore.Api;

[ApiController]
public class TestScoreController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
    
    public Task<IActionResult> UploadCsv()
    {
        return Task.FromResult<IActionResult>(Ok());
    } 
}