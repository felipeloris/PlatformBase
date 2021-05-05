using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using System.Collections.Generic;

namespace Loris.Entities
{
    public partial class AuthResource : AuditRegister, IEntityIdInt
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Dictionary { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AuthRoleResource> AuthRoleResource { get; set; }
    }
}