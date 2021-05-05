using Loris.Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loris.Common.EF.Context
{
    public static class AuditRegisterConfiguration
    {
        public static void Configure<t>(EntityTypeBuilder<t> entity) where t : class, IAuditRegister
        {
            entity.Property(e => e.CtrlCreatedIn)
                .HasColumnName("ctrl_created_in");

            entity.Property(e => e.CtrlCreatedBy)
                .HasColumnName("ctrl_created_by")
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.CtrlModifiedIn)
                .HasColumnName("ctrl_modified_in");

            entity.Property(e => e.CtrlModifiedBy)
                .HasColumnName("ctrl_modified_by")
                .HasMaxLength(60)
                .IsUnicode(false);
        }
    }
}
