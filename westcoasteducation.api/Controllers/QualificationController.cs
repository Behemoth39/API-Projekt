using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.ViewModels;

namespace westcoasteducation.api.Controllers
{
    [ApiController]
    [Route("api/v1/qualifications")]
    public class QualificationController : ControllerBase
    {
        private readonly WestCoastEducationContext _context;
        public QualificationController(WestCoastEducationContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult> List()
        {
            var result = await _context.Qualifications
          .Select(q => new
          {
              Id = q.Id,
              Qualification = q.Qualification
          })
          .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _context.Qualifications
            .Select(q => new QualificationVIewModel
            {
                Qualification = q.Qualification
            })
            .SingleOrDefaultAsync(q => q.Id == id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var result = await _context.Qualifications
           .Select(q => new QualificationVIewModel
           {
               Qualification = q.Qualification
           })
           .SingleOrDefaultAsync(C => C.Qualification!.ToUpper().Trim() == name.ToUpper().Trim());
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> AddQualification()
        {
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQualification(int id)
        {
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult AddQualificationToTeacher(int id)
        {
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult RemoveQualificationFromTeacher(int id)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQualification(int id)
        {
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification is null) return NotFound($"Finns ingen kompetens med id: {id}");

            _context.Qualifications.Remove(qualification);
            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
        }
    }
}