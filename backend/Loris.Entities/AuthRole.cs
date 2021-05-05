using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using System.Collections.Generic;

namespace Loris.Entities
{
    public partial class AuthRole : AuditRegister, IEntityIdInt
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AuthRoleResource> AuthRoleResource { get; set; }

        public virtual ICollection<AuthUserRole> AuthUserRole { get; set; }
    }
}