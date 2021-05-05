using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using System.Collections.Generic;

namespace Loris.Entities
{
    public class CourierTemplate : AuditRegister, IEntityIdInt
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string TemplateName { get; set; }

        public string Title { get; set; }

        public string Template { get; set; }

        public string SystemSenderIdent { get; set; }

        public CourierSystem System { get; set; }

        public virtual ICollection<CourierMessage> Messages { get; set; }
    }
}
