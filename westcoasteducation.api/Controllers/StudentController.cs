using Microsoft.AspNetCore.Mvc;
using westcoasteducation.api.Data;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    public WestCoastEducationContext _context { get; }
    public StudentController(WestCoastEducationContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public ActionResult List()
    {
        return Ok(new { message = "Lista studenter fungerar" });
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(new { message = $"GetById fungerar {id}" });
    }

    [HttpGet("socialsecurity/{socialSecurity}")]
    public ActionResult GetBySocialSecurityNumber(string socialSecurity)
    {
        return Ok(new { message = $"GetBySocialSecurity fungerar {socialSecurity}" });
    }

    [HttpGet("email/{email}")]
    public ActionResult GetByEmail(string email)
    {
        return Ok(new { message = $"GetByEmail fungerar {email}" });
    }

    [HttpPost()]
    public ActionResult AddStudent()
    {
        return Created(nameof(GetById), new { message = "AddStudent fungerar" });
    }

    [HttpPut("{id}")]
    public ActionResult UpdateStudent(int id)
    {
        return NoContent();
    }

    [HttpGet()]
    public ActionResult ListCoursesTaken()
    {
        return Ok(new { message = "Lista anmälda kurser fungerar" });
    }

    [HttpPatch("{id}")]
    public ActionResult RegisterToCourse(int id)
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult RemoveFromCourse(int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteStudent(int id)
    {
        return NoContent();
    }
}
