using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;

namespace Loris.Services
{
    public class CourierAttachmentService : ServiceAsync<CourierAttachment, CourierAttachmentRep>, ICourierAttachmentService
    {
        public CourierAttachmentService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }
    }
}
