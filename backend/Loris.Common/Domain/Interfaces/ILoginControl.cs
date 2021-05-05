using System;

namespace Loris.Common.Domain.Interfaces
{
    public interface ILoginControl
    {
        string KeyChangePwd { get; set; }

        DateTime? DtBlocked { get; set; }

        DateTime? DtExpiredPwd { get; set; }

        short WrondPwdAttempts { get; set; }
    }
}
