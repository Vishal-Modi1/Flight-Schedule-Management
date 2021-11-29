using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM.Common;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ClassificationController : Controller
    {
        private readonly IClassificationService _classificationService;

        public ClassificationController(IClassificationService classificationService)
        {
            _classificationService = classificationService;
        }
        
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            CurrentResponse response = _classificationService.List();

            return Ok(response);
        }

    }
}
