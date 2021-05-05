using AutoMapper;
using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Common.Domain.Interfaces;
using Loris.Entities;
using Loris.Interfaces.Services;

namespace Loris.Application.ApplicationService
{
    public class CourierToAppService : BaseAppService<ICourierToService, CourierTo, CourierToDto>, ICourierToAppService
    {
        public CourierToAppService(ICourierToService service, IMapper mapper, ILoginManager loginManager)
            : base(service, mapper, loginManager)
        {
        }
    }
}