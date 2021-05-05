using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;
using Loris.Common.EF.Context;

namespace Loris.Infra.Context
{
    public class CourierTemplateConfiguration : IEntityTypeConfiguration<CourierTemplate>
    {
        public void Configure(EntityTypeBuilder<CourierTemplate> entity)
        {
            entity.ToTable("courier_template", "public")
                .HasKey(k => k.Id)
                .HasName("courier_template_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.ExternalId)
                .HasColumnName("external_id")
                .HasMaxLength(35)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.TemplateName)
                .HasColumnName("template_name")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Template)
                .HasColumnName("template")
                .HasConversion<string>()
                .HasColumnType("text")
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.SystemSenderIdent)
                .HasColumnName("system_sender_ident")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.System)
                .HasColumnName("system")
                .HasConversion<short>()
                .IsRequired();

            AuditRegisterConfiguration.Configure(entity);
        }
    }
}
