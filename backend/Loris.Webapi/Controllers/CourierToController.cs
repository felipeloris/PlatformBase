using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class CourierToController : DtoController<ICourierToAppService, CourierToDto>
    {
        public CourierToController(ICourierToAppService appService)
            : base(appService)
        {
        }
    }
}
