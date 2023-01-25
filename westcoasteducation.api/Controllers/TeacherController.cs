using Microsoft.AspNetCore.Mvc;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/teachers")]
public class TeacherController : ControllerBase
{
    [HttpGet()]
    public ActionResult List()
    {
        return Ok(new { message = "Lista lärare fungerar" });
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(new { message = $"GetById fungerar {id}" });
    }

    [HttpGet("email/{email}")]
    public ActionResult GetByEmail(string email)
    {
        return Ok(new { message = $"GetByEmail fungerar {email}" });
    }

    [HttpPost()]
    public ActionResult AddTeacher()
    {
        return Created(nameof(GetById), new { message = "AddTeacher fungerar" });
    }

    [HttpPut("{id}")]
    public ActionResult UpdateTeacher(int id)
    {
        return NoContent();
    }

    [HttpGet()]
    public ActionResult ListTaughtCourses()
    {
        return Ok(new { message = "Lista undervisade kurser fungerar" });
    }

    [HttpPatch("{id}")]
    public ActionResult RegisterToCourse(int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteStudent(int id)
    {
        return NoContent();
    }
}
