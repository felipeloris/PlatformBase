using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class ResourceController : DtoController<IResourceAppService, ResourceDto>
    {
        public ResourceController(IResourceAppService appService)
            : base(appService)
        {
        }
    }
}
