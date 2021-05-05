using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class CourierAttachmentController : DtoController<ICourierAttachmentAppService, CourierAttachmentDto>
    {
        public CourierAttachmentController(ICourierAttachmentAppService appService)
            : base(appService)
        {
        }
    }
}
