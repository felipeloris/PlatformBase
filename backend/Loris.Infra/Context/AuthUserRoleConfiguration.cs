using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;
using Loris.Common.EF.Context;

namespace Loris.Infra.Context
{
    public class AuthUserRoleConfiguration : IEntityTypeConfiguration<AuthUserRole>
    {
        public void Configure(EntityTypeBuilder<AuthUserRole> entity)
        {
            entity.ToTable("auth_user_role", "public")
                .HasKey(k => new { k.AuthRoleId, k.AuthUserId })
                .HasName("auth_user_role_pk");

            entity.Property(e => e.AuthRoleId)
                .HasColumnName("auth_role_id")
                .IsRequired();

            entity.HasOne(d => d.AuthRole)
                .WithMany(p => p.AuthUserRole)
                .HasForeignKey(d => d.AuthRoleId)
                .HasConstraintName("auth_user_role_fk_auth_role")
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.AuthUserId)
                .HasColumnName("auth_user_id")
                .IsRequired();

            entity.HasOne(d => d.AuthUser)
                .WithMany(p => p.AuthUserRole)
                .HasForeignKey(d => d.AuthUserId)
                .HasConstraintName("auth_user_role_fk_auth_user")
                .OnDelete(DeleteBehavior.ClientSetNull);

            AuditRegisterConfiguration.Configure(entity);
        }
    }
}
