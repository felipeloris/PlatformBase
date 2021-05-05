namespace Loris.Common.Webapi.Domain.Entities
{
    public class SwaggerSettings
    {
        public string Id { get; set; } = "Bearer";

        public string Name { get; set; } = "Authorization";

        public string Scheme { get; set; } = "Bearer";

        public string BearerFormat { get; set; } = "JWT";

        public string Description { get; set; } = "JWT Authorization header using the Bearer scheme.\r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"";
    }
}
