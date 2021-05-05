using Loris.Common;
using Loris.Common.Domain.Entities;

namespace Loris.Entities
{
    public partial class AuthRoleResource : AuditRegister
    {
        public int AuthRoleId { get; set; }

        public virtual AuthRole AuthRole { get; set; }

        public int AuthResourceId { get; set; }

        public virtual AuthResource AuthResource { get; set; }

        public AccessPermission Permissions { get; set; }
    }
}