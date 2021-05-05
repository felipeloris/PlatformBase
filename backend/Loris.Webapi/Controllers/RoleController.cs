using Loris.Application.Dtos;
using Loris.Application.Interfaces;

namespace Loris.Webapi.Controllers
{
    public class RoleController : DtoController<IRoleAppService, RoleDto>
    {
        public RoleController(IRoleAppService appService)
            : base(appService)
        {
        }
    }
}
