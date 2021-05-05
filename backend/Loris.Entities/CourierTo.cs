using Loris.Common;
using Loris.Common.Domain.Interfaces;
using System;

namespace Loris.Entities
{
    public class CourierTo : IEntityIdLong
    {
        public long Id { get; set; }

        public long CourierMessageId { get; set; }

        public virtual CourierMessage CourierMessage { get; set; }

        public string SystemUserIdent { get; set; }

        public CourierSystem System { get; set; }

        public CourierStatus Status { get; set; }

        public DateTime LastProcessing { get; set; }
    }
}
