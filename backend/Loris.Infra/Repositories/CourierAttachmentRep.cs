using Loris.Common.EF.Repository;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;

namespace Loris.Infra.Repositories
{
    public sealed class CourierAttachmentRep : RepositoryAsync<CourierAttachment, LorisContext>
    {
        public CourierAttachmentRep(IDatabase database) : base(database) 
        { 
        }

        public CourierAttachmentRep(LorisContext context) : base(context) 
        { 
        }
    }
}
