using AutoMapper;
using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Common.Domain.Interfaces;
using Loris.Entities;
using Loris.Interfaces.Services;

namespace Loris.Application.ApplicationService
{
    public class ResourceAppService : BaseAppService<IAuthResourceService, AuthResource, ResourceDto>, IResourceAppService
    {
        public ResourceAppService(IAuthResourceService service, IMapper mapper, ILoginManager loginManager)
            : base(service, mapper, loginManager)
        {
        }
    }
}