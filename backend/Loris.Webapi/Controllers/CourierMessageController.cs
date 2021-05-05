using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class CourierMessageController : DtoController<ICourierMessageAppService, CourierMessageDto>
    {
        public CourierMessageController(ICourierMessageAppService appService)
            : base(appService)
        {
        }
    }
}
