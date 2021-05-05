using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class CourierAttachmentConfiguration : IEntityTypeConfiguration<CourierAttachment>
    {
        public void Configure(EntityTypeBuilder<CourierAttachment> entity)
        {
            entity.ToTable("courier_attachment", "public")
                .HasKey(k => k.Id)
                .HasName("courier_attachment_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.CourierMessageId)
                .HasColumnName("courier_message_id")
                .IsRequired();

            entity.HasOne(d => d.CourierMessage)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.CourierMessageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("courier_attachment_fk_courier_message");

            entity.Property(e => e.FileName)
                .HasColumnName("file_name")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.FileType)
                 .HasColumnName("file_type")
                 .HasConversion<short>()
                 .IsRequired();

            entity.Property(e => e.File)
                .HasColumnName("file")
                .IsRequired();
        }
    }
}
