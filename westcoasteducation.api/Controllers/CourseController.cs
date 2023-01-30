using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.Models;
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
        .Include(s => s.Students)
        .Include(t => t.Teachers)
        .Select(c => new CourseListViewModel
        {
            Id = c.Id,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            //Students = c.Students.Select(slv => new StudentListViewModel { StudentEmail = slv. })  // jag fattar ej
            //Teachers = c.Teachers.Select(tlv => new TeacherListViewModel {  })
        })
        .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Courses
        .Include(s => s.Students)
        .Include(t => t.Teachers)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            //Students = c.Students.Select(slv => new StudentListViewModel { StudentEmail = slv. })  // jag fattar ej
            //Teachers = c.Teachers.Select(tlv => new TeacherListViewModel {  })
        })
        .SingleOrDefaultAsync(C => C.Id == id);
        return Ok(result);
    }

    [HttpGet("coursenr/{courseNr}")]
    public async Task<ActionResult> GetByCourseNumber(string courseNr)
    {
        var result = await _context.Courses
        .Include(s => s.Students)
        .Include(t => t.Teachers)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            //Students = c.Students.Select(slv => new StudentListViewModel { StudentEmail = slv. })  // jag fattar ej
            //Teachers = c.Teachers.Select(tlv => new TeacherListViewModel {  })
        })
        .SingleOrDefaultAsync(C => C.CourseNumber!.ToUpper().Trim() == courseNr.ToUpper().Trim());
        return Ok(result);
    }

    [HttpGet("coursetitle/{courseTitle}")]
    public async Task<ActionResult> GetByTitle(string courseTitle)
    {
        var result = await _context.Courses
        .Include(s => s.Students)
        .Include(t => t.Teachers)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            //Students = c.Students.Select(slv => new StudentListViewModel { StudentEmail = slv. })  // jag fattar ej
            //Teachers = c.Teachers.Select(tlv => new TeacherListViewModel {  })
        })
        .SingleOrDefaultAsync(C => C.CourseTitle!.ToUpper().Trim() == courseTitle.ToUpper().Trim());
        return Ok(result);
    }

    [HttpGet("startdate/{startDate}")]
    public async Task<ActionResult> GetBystartDate(DateOnly startDate)
    {
        var result = await _context.Courses
        .Include(s => s.Students)
        .Include(t => t.Teachers)
        .Where(C => C.CourseStartDate == startDate)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            //Students = c.Students.Select(slv => new StudentListViewModel { StudentEmail = slv. })  // jag fattar ej
            //Teachers = c.Teachers.Select(tlv => new TeacherListViewModel {  })
        })
        .ToListAsync();
        return Ok(result);
    }

    [HttpPost()]
    public async Task<ActionResult> AddCourse(CourseAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas, kontrollera så att allt stämmer");

        var exists = await _context.Courses.SingleOrDefaultAsync(c => c.CourseTitle!.ToUpper().Trim() == model.CourseTitle!.ToUpper().Trim());

        if (exists is not null) return BadRequest($"En kurs med {model.CourseTitle} finns redan");

        var course = new CourseModel
        {
            CourseNumber = model.CourseNumber,
            CourseTitle = model.CourseTitle,
            CourseStartDate = model.CourseStartDate
        };

        await _context.Courses.AddAsync(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = course.Id });
        }

        return StatusCode(500, "Internal Server Error");
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
