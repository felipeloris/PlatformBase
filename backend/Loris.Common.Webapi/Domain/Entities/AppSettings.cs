using System.Collections.Generic;

namespace Loris.Common.Webapi.Domain.Entities
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }

        public SwaggerSettings Swagger { get; set; }

        public LorisJwtSettings LorisConfig { get; set; }

        public AboutSettings About { get; } = new AboutSettings();

        public int DbContextPoolSize { get; set; } = 128;

        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
