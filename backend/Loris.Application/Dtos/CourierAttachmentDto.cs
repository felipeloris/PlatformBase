using Loris.Common;

namespace Loris.Application.Dtos
{
    public class CourierAttachmentDto
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public FileType FileType { get; set; }

        public byte[] File { get; set; }
    }
}
