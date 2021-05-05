using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class CourierToConfiguration : IEntityTypeConfiguration<CourierTo>
    {
        public void Configure(EntityTypeBuilder<CourierTo> entity)
        {
            entity.ToTable("courier_to", "public")
                .HasKey(k => k.Id)
                .HasName("courier_to_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.CourierMessageId)
                .HasColumnName("courier_message_id")
                .IsRequired();

            entity.HasOne(d => d.CourierMessage)
                .WithMany(p => p.To)
                .HasForeignKey(d => d.CourierMessageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("courier_to_fk_courier_message");

            entity.Property(e => e.SystemUserIdent)
                .HasColumnName("system_user_ident")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.System)
                .HasColumnName("system")
                .HasConversion<short>()
                .IsRequired();

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasConversion<short>()
                .IsRequired();

            entity.Property(e => e.LastProcessing)
                .HasColumnName("last_processing")
                .HasColumnType("timestamp without time zone");
        }
    }
}
