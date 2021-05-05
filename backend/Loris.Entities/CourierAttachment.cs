using Loris.Common;
using Loris.Common.Domain.Interfaces;

namespace Loris.Entities
{
    public class CourierAttachment : IEntityIdLong
    {
        public long Id { get; set; }

        public long CourierMessageId { get; set; }

        public virtual CourierMessage CourierMessage { get; set; }

        public string FileName { get; set; }

        public FileType FileType { get; set; }

        public byte[] File { get; set; }
    }
}
