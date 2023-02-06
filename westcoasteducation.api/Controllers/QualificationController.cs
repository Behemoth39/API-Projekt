using Microsoft.AspNetCore.Mvc;
using westcoasteducation.api.Data;

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
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return NoContent();
        }

        [HttpGet("qualification/{qualification}")]
        public async Task<ActionResult> GetByName(string qualification)
        {
            return NoContent();
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
        public async Task<ActionResult> DeleteStudent(int id)
        {
            return NoContent();
        }
    }
}