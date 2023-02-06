using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.Models;
using westcoasteducation.api.ViewModels;

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
    public async Task<ActionResult> List()
    {
        var result = await _context.Students
        .Select(s => new StudentListViewModel
        {
            FirstName = s.FirstName
        })
        .ToListAsync();
        return Ok(result);
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
    public async Task<ActionResult> AddStudent(StudentAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas, kontrollera så att allt stämmer");

        var exists = await _context.Students.SingleOrDefaultAsync(s => s.Email!.ToUpper().Trim() == model.Email!.ToUpper().Trim());

        if (exists is not null) return BadRequest($"En person med email {model.Email} finns redan");

        var student = new StudentModel
        {
            Age = model.Age,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone
        };

        await _context.Students.AddAsync(student);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = student.Id });
        }

        return StatusCode(500, "Internal Server Error");
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
