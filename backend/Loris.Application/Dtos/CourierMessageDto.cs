using Loris.Common;
using System;
using System.Collections.Generic;

namespace Loris.Application.Dtos
{
    public partial class CourierMessageDto
    {
        public long Id { get; set; }

        public CourierAction Action { get; set; }

        public InternalSystem Generator { get; set; }

        public CourierTemplateDto CourierTemplate { get; set; }

        public string Message { get; set; }

        public DateTime DtInclusion { get; set; }

        public string From { get; set; }

        public virtual ICollection<CourierToDto> To { get; set; }

        public virtual ICollection<CourierAttachmentDto> Attachments { get; set; }
    }
}
