using System;

namespace Loris.Common.Exceptions
{
    public class RecordChangedException : BusinessException
    {
        public string ModifiedBy { get; private set; }

        public DateTime ModifiedIn { get; private set; }

        public RecordChangedException(string modifiedBy, DateTime modifiedIn)
            : base(string.Format(Messages.RecordAltered(), modifiedBy, modifiedIn))
        {
            ModifiedBy = modifiedBy;
            ModifiedIn = modifiedIn;
        }
    }
}
