using AutoMapper;
using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Common.Domain.Interfaces;
using Loris.Entities;
using Loris.Interfaces.Services;

namespace Loris.Application.ApplicationService
{
    public class RoleAppService : BaseAppService<IAuthRoleService, AuthRole, RoleDto>, IRoleAppService
    {
        public RoleAppService(IAuthRoleService service, IMapper mapper, ILoginManager loginManager)
            : base(service, mapper, loginManager)
        { 
        }
    }
}