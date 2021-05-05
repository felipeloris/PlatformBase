using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;

namespace Loris.Services
{
    public class CourierToService : ServiceAsync<CourierTo, CourierToRep>, ICourierToService
    {
        public CourierToService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }
    }
}
