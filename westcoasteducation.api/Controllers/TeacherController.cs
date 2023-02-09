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
            Age = t.Age,
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
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Teachers
        .Select(t => new TeacherListViewModel
        {
            Age = t.Age,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            Phone = t.Phone,
            Courses = t.Courses!.Select(clv => new CourseListViewModel { CourseTitle = clv.CourseTitle }).ToList(),
            Qualifications = t.Qualifications!.Select(qlv => new QualificationVIewModel { Qualification = qlv.Qualification }).ToList(),
        })
        .SingleOrDefaultAsync(t => t.Id == id);
        return Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult> GetByEmail(string email)
    {
        var result = await _context.Teachers
        .Select(t => new TeacherListViewModel
        {
            Age = t.Age,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            Phone = t.Phone,
            Courses = t.Courses!.Select(clv => new CourseListViewModel { CourseTitle = clv.CourseTitle }).ToList(),
            Qualifications = t.Qualifications!.Select(qlv => new QualificationVIewModel { Qualification = qlv.Qualification }).ToList(),
        })
        .SingleOrDefaultAsync(t => t.Email!.ToUpper().Trim() == email.ToUpper().Trim());
        return Ok(result);
    }

    [HttpPost()]
    public async Task<ActionResult> AddTeacher(TeacherAddViewModel model) //behövs 2 separata models för update och add
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas, kontrollera så att allt stämmer");

        var exists = await _context.Teachers.SingleOrDefaultAsync(s => s.Email!.ToUpper().Trim() == model.Email!.ToUpper().Trim());

        if (exists is not null) return BadRequest($"En lärare med email {model.Email} finns redan");

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
            return CreatedAtAction(nameof(GetById),
            new { id = teacher.Id },
            new { id = teacher.Id, Name = teacher.FirstName + teacher.LastName }); // testa
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher(int id, TeacherUpdateViewModel model) //behövs 2 separata models för update och add
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas");

        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null) return BadRequest($"Läraren {model.FirstName} {model.LastName} finns inte");

        teacher.Age = model.Age;
        teacher.FirstName = model.FirstName;
        teacher.LastName = model.LastName;
        teacher.Email = model.Email;
        teacher.Phone = model.Phone;

        _context.Teachers.Update(teacher);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> ListTaughtCourses(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null) return BadRequest($"Kunde inte hitta läraren i systemet");

        var result = await _context.Teachers
       .Select(t => new TeacherListViewModel
       {
           Courses = t.Courses!.Select(clv => new CourseListViewModel { CourseTitle = clv.CourseTitle }).ToList()
       })
       .ToListAsync();
        return Ok(result);
    }

    [HttpPatch("registertocourse/{id}")] // namnge bättre
    public async Task<ActionResult> RegisterToCourse(int courseId, int teacherId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null) return BadRequest("Kunde inte hitta kursen i systemet.");

        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher is null) return BadRequest($"Kunde inte hitta läraren i systemet");

        course.Teacher = teacher;

        _context.Courses.Update(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpPatch("removefromcourse/{courseId}/{teacherId}")]
    public async Task<ActionResult> RemoveFromCourse(int courseId, int teacherId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null) return BadRequest("kursen är inte i systemet.");

        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher is null) return BadRequest($"läraren är inte i systemet");

        course.Teacher = null; // funkar ?

        _context.Courses.Update(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
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
