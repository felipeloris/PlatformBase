using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;
using System.Threading.Tasks;

namespace Loris.Services
{
    public class CourierTemplateService : ServiceAsync<CourierTemplate, CourierTemplateRep>, ICourierTemplateService
    {
        public CourierTemplateService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }

        public const string SEND_TOKEN = "SYS.COURIER.SENDTOKEN";

        public async Task<CourierTemplate> GetByExternalId(string externalId)
        {
            if (string.IsNullOrEmpty(externalId?.Trim()))
                return null;

            var template = await Repository.GetByExternalId(externalId);

            return template;
        }
    }
}
