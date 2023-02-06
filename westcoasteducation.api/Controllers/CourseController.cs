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
        .Include(t => t.Teacher)
        .Select(c => new CourseListViewModel
        {
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            CourseEndDate = c.CourseEndDate,
            Teacher = c.Teacher.FirstName,
            Students = c.Students!.Select(slv => new StudentListViewModel { FirstName = slv.FirstName }).ToList(),
        })
        .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            CourseEndDate = c.CourseEndDate,
            Teacher = c.Teacher.Email,
            Students = c.Students!.Select(slv => new StudentListViewModel { Email = slv.Email }).ToList()
        })
        .SingleOrDefaultAsync(C => C.Id == id);
        return Ok(result);
    }

    [HttpGet("coursenr/{courseNr}")]
    public async Task<ActionResult> GetByCourseNumber(string courseNr)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            CourseEndDate = c.CourseEndDate,
            Teacher = c.Teacher.Email,
            Students = c.Students!.Select(slv => new StudentListViewModel { Email = slv.Email }).ToList()
        })
        .SingleOrDefaultAsync(C => C.CourseNumber!.ToUpper().Trim() == courseNr.ToUpper().Trim());
        return Ok(result);
    }

    [HttpGet("coursetitle/{courseTitle}")]
    public async Task<ActionResult> GetByTitle(string courseTitle)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            CourseEndDate = c.CourseEndDate,
            Teacher = c.Teacher.Email,
            Students = c.Students!.Select(slv => new StudentListViewModel { Email = slv.Email }).ToList()
        })
        .SingleOrDefaultAsync(C => C.CourseTitle!.ToUpper().Trim() == courseTitle.ToUpper().Trim());
        return Ok(result);
    }

    [HttpGet("startdate/{startDate}")]
    public async Task<ActionResult> GetBystartDate(DateOnly startDate)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Where(C => C.CourseStartDate == startDate)
        .Select(c => new CourseDetailListViewModel
        {
            Id = c.Id,
            Status = c.Status,
            CourseNumber = c.CourseNumber,
            CourseTitle = c.CourseTitle,
            CourseStartDate = c.CourseStartDate,
            CourseEndDate = c.CourseEndDate,
            Teacher = c.Teacher.Email,
            Students = c.Students!.Select(slv => new StudentListViewModel { Email = slv.Email }).ToList()
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
            CourseStartDate = model.CourseStartDate,
            CourseEndDate = model.CourseEndDate
        };

        await _context.Courses.AddAsync(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = course.Id });
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, CourseUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas");

        var course = await _context.Courses.FindAsync(id);
        if (course is null) return BadRequest($"En kurs med {model.CourseTitle} finns inte");

        course.CourseNumber = model.CourseNumber;
        course.CourseTitle = model.CourseTitle;
        course.CourseStartDate = model.CourseStartDate;
        course.CourseEndDate = model.CourseEndDate;

        _context.Courses.Update(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpPatch("full/{id}")]
    public async Task<ActionResult> MarkCourseAsFull(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null) return NotFound($"Finns ingen kurs med id: {id}");
        course.Status = CourseStatusEnum.Full;

        _context.Courses.Update(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpPatch("completed/{id}")]
    public async Task<ActionResult> MarkCourseCompleted(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null) return NotFound($"Finns ingen kurs med id: {id}");
        course.Status = CourseStatusEnum.Completed;

        _context.Courses.Update(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null) return NotFound($"Finns ingen kurs med id: {id}");

        _context.Courses.Remove(course);
        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }
}
