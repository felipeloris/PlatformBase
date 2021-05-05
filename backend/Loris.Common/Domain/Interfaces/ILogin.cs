using System;

namespace Loris.Common.Domain.Interfaces
{
    /// <summary>
    /// Interface da estrutura de Login
    /// </summary>
    public interface ILogin
    {
        string ExtenalId { get; set; }

        Languages Language { get; set; }

        LoginType LoginType { get; }

        LoginStatus LoginStatus { get; set; }

        DateTime LoginAt { get; set; }

        string SessionId { get; set; }
    }
}
