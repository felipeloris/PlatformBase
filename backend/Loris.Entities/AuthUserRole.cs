using Loris.Common.Domain.Entities;

namespace Loris.Entities
{
    public partial class AuthUserRole : AuditRegister
    {
        public int AuthRoleId { get; set; }

        public virtual AuthRole AuthRole { get; set; }

        public int AuthUserId { get; set; }

        public virtual AuthUser AuthUser { get; set; }
    }
}