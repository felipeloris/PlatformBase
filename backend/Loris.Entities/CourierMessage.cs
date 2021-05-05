using Loris.Common;
using Loris.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Loris.Entities
{
    public partial class CourierMessage : IEntityIdLong
    {
        public long Id { get; set; }

        public CourierAction Action { get; set; }

        public InternalSystem Generator { get; set; }

        public int? CourierTemplateId { get; set; }

        public CourierTemplate CourierTemplate { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime DtInclusion { get; set; }

        public string From { get; set; }

        public virtual ICollection<CourierTo> To { get; set; }

        public virtual ICollection<CourierAttachment> Attachments { get; set; }
    }
}
