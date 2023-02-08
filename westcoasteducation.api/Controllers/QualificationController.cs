using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoasteducation.api.Data;
using westcoasteducation.api.Models;
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
        public async Task<ActionResult> AddQualification(QualificationAddVIewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Information saknas, kontrollera så att allt stämmer");

            var exists = await _context.Qualifications.SingleOrDefaultAsync(s => s.Qualification!.ToUpper().Trim() == model.Qualification!.ToUpper().Trim());

            if (exists is not null) return BadRequest($"{model.Qualification} finns redan");

            var qualification = new QualificationModel
            {
                Qualification = model.Qualification
            };

            await _context.Qualifications.AddAsync(qualification);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Created(nameof(GetById), new { id = qualification.Id });
            }

            return StatusCode(500, "Internal Server Error");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQualification(int id, QualificationAddVIewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Information saknas");

            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification is null) return BadRequest($"{model.Qualification} finns inte");

            qualification.Qualification = model.Qualification;

            _context.Qualifications.Update(qualification);
            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> AddQualificationToTeacher(int id) // förmodligen ska en model följa med
        {
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification is null) return NotFound($"Finns ingen kompetens med id: {id}");
            // sätt värdet här

            _context.Qualifications.Update(qualification);
            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> RemoveQualificationFromTeacher(int id) // förmodligen ska en model följa med
        {
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification is null) return NotFound($"Finns ingen kompetens med id: {id}");
            // sätt värdet här

            _context.Qualifications.Update(qualification);
            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
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