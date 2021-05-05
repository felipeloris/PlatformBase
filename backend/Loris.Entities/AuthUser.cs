using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Loris.Entities
{
    public partial class AuthUser : AuditRegister, IEntityIdInt, ILoginPwd, ILoginControl
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        #region Interface LoginPwd

        public string ExtenalId { get; set; }

        public string Password { get; set; }

        public string EncryptedPassword { get; set; }

        public Languages Language { get; set; } = Languages.Portuguese;

        public LoginType LoginType => LoginType.User;

        public LoginStatus LoginStatus { get; set; }

        public DateTime LoginAt { get; set; }

        public string SessionId { get; set; }

        #endregion

        public DateTime? LogoutAt { get; set; }

        public string Note { get; set; }

        #region Interface ILoginControl

        public string KeyChangePwd { get; set; }

        public DateTime? DtBlocked { get; set; }

        public DateTime? DtExpiredPwd { get; set; }

        public short WrondPwdAttempts { get; set; }

        #endregion

        public virtual ICollection<AuthUserRole> AuthUserRole { get; set; }
    }
}