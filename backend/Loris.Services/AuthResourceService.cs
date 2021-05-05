using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;

namespace Loris.Services
{
    public class AuthResourceService : ServiceAsync<AuthResource, AuthResourceRep>, IAuthResourceService
    {
        public AuthResourceService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }
    }
}
