using Loris.CrossCutting;
using Loris.Common.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Loris.Common;

namespace Loris.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalizationController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(Internationalization.GetAll());
        }

        [HttpGet("GetByLanguage")]
        [OpenApiOperation("GetByLanguage")]
        public ActionResult GetByLanguage(Languages language)
        {
            return Ok(Internationalization.GetAllByLanguage(language));
        }
    }
}
