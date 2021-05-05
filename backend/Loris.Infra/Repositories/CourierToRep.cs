using Loris.Common.EF.Repository;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;

namespace Loris.Infra.Repositories
{
    public sealed class CourierToRep : RepositoryAsync<CourierTo, LorisContext>
    {
        public CourierToRep(IDatabase database) : base(database) 
        { 
        }

        public CourierToRep(LorisContext context) : base(context) 
        { 
        }
    }
}
