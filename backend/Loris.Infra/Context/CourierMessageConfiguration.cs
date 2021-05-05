using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class CourierMessageConfiguration : IEntityTypeConfiguration<CourierMessage>
    {
        public void Configure(EntityTypeBuilder<CourierMessage> entity)
        {
            entity.ToTable("courier_message", "public")
                .HasKey(k => k.Id)
                .HasName("courier_message_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.Action)
                .HasColumnName("action")
                .HasConversion<short>();

            entity.Property(e => e.Generator)
                 .HasColumnName("generator")
                 .HasConversion<short>();

            entity.Property(e => e.CourierTemplateId)
                .HasColumnName("courier_template_id");

            entity.HasOne(d => d.CourierTemplate)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.CourierTemplateId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("courier_message_fk_courier_template");

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Message)
                .HasColumnName("message")
                .HasConversion<string>()
                .HasColumnType("text")
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.DtInclusion)
                .HasColumnName("dt_inclusion")
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            entity.Property(e => e.From)
                .HasColumnName("from")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
