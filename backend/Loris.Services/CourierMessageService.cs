using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;

namespace Loris.Services
{
    public class CourierMessageService : ServiceAsync<CourierMessage, CourierMessageRep>, ICourierMessageService
    {
        public CourierMessageService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }
    }
}
