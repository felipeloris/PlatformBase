using Loris.Common.EF.Repository;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;

namespace Loris.Infra.Repositories
{
    public sealed class AuthResourceRep : RepositoryAsync<AuthResource, LorisContext>
    {
        public AuthResourceRep(IDatabase database) : base(database) 
        { 
        }

        public AuthResourceRep(LorisContext context) : base(context) 
        { 
        }
    }
}
