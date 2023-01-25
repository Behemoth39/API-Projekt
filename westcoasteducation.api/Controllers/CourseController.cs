using Microsoft.AspNetCore.Mvc;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CourseController : ControllerBase
{
    [HttpGet()]
    public ActionResult List()
    {
        return Ok(new { message = "Lista kurser fungerar" });
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(new { message = $"GetById fungerar {id}" });
    }

    [HttpGet("coursenr/{courseNr}")]
    public ActionResult GetByCourseNumber(string courseNr)
    {
        return Ok(new { message = $"GetByCourseNumber fungerar {courseNr}" });
    }

    [HttpGet("coursetitle/{courseTitle}")]
    public ActionResult GetByStartDate(string courseTitle)
    {
        return Ok(new { message = $"GetBycourseTitle fungerar {courseTitle}" });
    }

    [HttpGet("startdate/{startDate}")]
    public ActionResult GetBystartDate(DateOnly startDate)
    {
        return Ok(new { message = $"GetBystartDate fungerar {startDate}" });
    }

    [HttpPost()]
    public ActionResult AddCourse()
    {
        return Created(nameof(GetById), new { message = "AddCourse fungerar" });
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCourse(int id)
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult MarkCourseAsFull(int id)
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult MarkCourseCompleted(int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCourse(int id)
    {
        return NoContent();
    }

}