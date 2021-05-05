using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class CourierTemplateController : DtoController<ICourierTemplateAppService, CourierTemplateDto>
    {
        public CourierTemplateController(ICourierTemplateAppService appService)
            : base(appService)
        {
        }
    }
}
