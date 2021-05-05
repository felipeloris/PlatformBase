using System;

namespace Loris.Common.Webapi.Domain.Entities
{
    public class JwtResult
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Token Expires in
        /// </summary>
        public int TokenExpiresIn { get; set; }

        /// <summary>
        /// Date of login
        /// </summary>
        public DateTime LoginAt { get; set; }
    }
}
