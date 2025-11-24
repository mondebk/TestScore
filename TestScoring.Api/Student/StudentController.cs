using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestScoring.Application.Interfaces;
using TestScoring.Application.Models;

namespace TestScoring.Api.Student;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ITestScoreRetrievalService _testScoreRetrievalService;

    public StudentController(ITestScoreRetrievalService testScoreRetrievalService)
    {
        _testScoreRetrievalService = testScoreRetrievalService;
    }

    [HttpGet("/{studentName}")]
    [ProducesResponseType(typeof(IEnumerable<StudentScoreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<StudentScoreResponse>>> SearchByName(
        string studentName,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(studentName))
        {
            return BadRequest("Student name cannot empty.");
        }

        try
        {
            var studentResults = await _testScoreRetrievalService
                .SearchScoreByStudentName(studentName, cancellationToken);

            if (studentResults == null)
            {
                return NoContent();
            }

            return Ok(studentResults);
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal error occured");
        }
    }
}