using Microsoft.AspNetCore.Mvc;

namespace TestScore.Api.Student;

[ApiController]
public class StudentController : ControllerBase
{
    //private IStudentService studentService;

    // public StudentController(IStudentService studentService)
    // {
    //     this.studentService = studentService;
    // }

    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}