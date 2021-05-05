using Loris.Entities;
using Loris.Common.Domain.Interfaces;
using System.Threading.Tasks;

namespace Loris.Interfaces.Services
{
    public interface ICourierTemplateService : IServiceAsync<CourierTemplate>
    {
        Task<CourierTemplate> GetByExternalId(string externalid);
    }
}