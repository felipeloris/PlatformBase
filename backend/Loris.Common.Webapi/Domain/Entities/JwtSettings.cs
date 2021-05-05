using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Loris.Common.Webapi.Domain.Entities
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpireMinutes { get; set; }
    }
}
