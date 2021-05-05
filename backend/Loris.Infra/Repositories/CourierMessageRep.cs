using Loris.Common.EF.Repository;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;

namespace Loris.Infra.Repositories
{
    public sealed class CourierMessageRep : RepositoryAsync<CourierMessage, LorisContext>
    {
        public CourierMessageRep(IDatabase database) : base(database) 
        { 
        }

        public CourierMessageRep(LorisContext context) : base(context) 
        { 
        }
    }
}
