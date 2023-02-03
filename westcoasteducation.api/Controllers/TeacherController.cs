using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.ViewModels;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/teachers")]
public class TeacherController : ControllerBase
{
    public WestCoastEducationContext _context { get; }
    public TeacherController(WestCoastEducationContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult> List()
    {
        var result = await _context.Teachers
        .Select(t => new TeacherListViewModel
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            Phone = t.Phone,
            Courses = t.Courses!.Select(clv => new CourseListViewModel { CourseTitle = clv.CourseTitle }).ToList(),
            Qualifications = t.Qualifications!.Select(qlv => new QualificationVIewModel { Qualification = qlv.Qualification }).ToList(),
        })
        .ToListAsync();
        return Ok(result);
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

    [HttpPatch("{id}")]
    public ActionResult RemoveFromCourse(int id)
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult AddQualification(int id)
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult RemoveQualification(int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null) return NotFound($"Finns ingen kurs med id: {id}");

        _context.Teachers.Remove(teacher);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }
}
