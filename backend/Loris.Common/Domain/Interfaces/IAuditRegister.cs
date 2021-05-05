namespace Loris.Common.Domain.Interfaces
{
    public interface IAuditRegister
    {
        long? CtrlCreatedIn { get; set; }

        string CtrlCreatedBy { get; set; }

        long? CtrlModifiedIn { get; set; }

        string CtrlModifiedBy { get; set; }
    }
}
