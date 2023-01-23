using Microsoft.AspNetCore.Mvc;

namespace westcoasteducation.api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CourseController : ControllerBase
{
    [HttpGet()]
    public ActionResult List()
    {
        return Ok(new { message = "Lista kurser fungerar" });
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(new { message = $"GetById fungerar {id}" });
    }
}
