using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.ViewModels;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CourseController : ControllerBase
{
    public WestCoastEducationContext _context { get; }
    public CourseController(WestCoastEducationContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult> List()
    {
        var result = await _context.Courses
        .Include(s => s.Student)
        .Include(t => t.Teacher)
        .Select(c => new CourseListViewModel
        {
            Id = c.Id,
            Student = c.Student.StudentEmail ?? "",
            Teacher = c.Teacher.TeacherEmail ?? "",
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            Status = c.Status,
            CourseStartDate = c.CourseStartDate
        })
        .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Courses.FindAsync(id);
        return Ok(result);
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
