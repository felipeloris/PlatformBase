using System;

namespace Loris.Common.Domain.Interfaces
{
    public interface ILoginPwd : ILogin
    {
        string Password { get; set; }

        string EncryptedPassword { get; set; }
    }
}
