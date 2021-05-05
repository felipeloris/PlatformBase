using AutoMapper;
using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Common.Domain.Interfaces;
using Loris.Entities;
using Loris.Interfaces.Services;

namespace Loris.Application.ApplicationService
{
    public class CourierAttachmentAppService : BaseAppService<ICourierAttachmentService, CourierAttachment, CourierAttachmentDto>, ICourierAttachmentAppService
    {
        public CourierAttachmentAppService(ICourierAttachmentService service, IMapper mapper, ILoginManager loginManager)
            : base(service, mapper, loginManager)
        {
        }
    }
}