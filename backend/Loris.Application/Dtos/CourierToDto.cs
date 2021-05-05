using Loris.Common;
using System;

namespace Loris.Application.Dtos
{
    public class CourierToDto
    {
        public long Id { get; set; }

        public string ExternalId { get; set; }

        public CourierSystem System { get; set; }

        public CourierStatus Status { get; set; }

        public DateTime LastProcessing { get; set; }
    }
}
