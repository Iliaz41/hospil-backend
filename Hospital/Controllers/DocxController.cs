using Hospital.Dtos.Employee_dto;
using Hospital.Services;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocxController : Controller
    {
        private readonly IDocxService _docxService;
        public DocxController(IDocxService docxService)
        {
            _docxService = docxService;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDocx(long id)
        {
            await _docxService.CreateDocument(id);
            return Ok();
        }
    }
}
