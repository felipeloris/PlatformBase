using Loris.Common.Domain.Interfaces;
using System;

namespace Loris.Common.Domain.Entities
{
    public class BasicLogin : ILogin
    {
        public string ExtenalId { get; set; }

        public Languages Language { get; set; }

        public LoginType LoginType { get; }

        public LoginStatus LoginStatus { get; set; }

        public DateTime LoginAt { get; set; }
        
        public string SessionId { get; set; }
    }
}
