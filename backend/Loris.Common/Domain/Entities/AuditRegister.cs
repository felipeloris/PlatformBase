using Loris.Common.Domain.Interfaces;

namespace Loris.Common.Domain.Entities
{
    public class AuditRegister : IAuditRegister
    {
        public virtual long? CtrlCreatedIn { get; set; }

        public virtual string CtrlCreatedBy { get; set; }

        public virtual long? CtrlModifiedIn { get; set; }

        public virtual string CtrlModifiedBy { get; set; }
    }
}
