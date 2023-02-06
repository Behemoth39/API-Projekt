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
    }
}