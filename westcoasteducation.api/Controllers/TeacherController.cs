using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.Models;
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
    public async Task<ActionResult> AddTeacher(TeacherAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas, kontrollera så att allt stämmer");

        var exists = await _context.Teachers.SingleOrDefaultAsync(s => s.Email!.ToUpper().Trim() == model.Email!.ToUpper().Trim());

        if (exists is not null) return BadRequest($"En person med email {model.Email} finns redan");

        var teacher = new TeacherModel
        {
            Age = model.Age,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone
        };

        await _context.Teachers.AddAsync(teacher);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = teacher.Id });
        }

        return StatusCode(500, "Internal Server Error");
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
