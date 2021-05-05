using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Common.EF.Context;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> entity)
        {
            entity.ToTable("auth_user", "public")
                .HasKey(k => k.Id)
                .HasName("auth_user_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                //http://www.npgsql.org/efcore/modeling/generated-properties.html#identity-sequence-options
                //.HasIdentityOptions(incrementBy: 1, startValue: 1, minValue: 1, maxValue: int.MaxValue, numbersToCache: 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.PersonId)
                .HasColumnName("person_id");

            entity.Property(e => e.Nickname)
                .HasColumnName("nickname")
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);
            entity.HasIndex(i => i.Email)
                .IsUnique()
                .HasDatabaseName("auth_user_ix_email");

            entity.Property(e => e.ExtenalId)
                .HasColumnName("external_id")
                .HasMaxLength(60)
                .IsRequired()
                .IsUnicode(false);
            entity.HasIndex(i => i.ExtenalId)
                .IsUnique()
                .HasDatabaseName("auth_user_ix_external_id");

            entity.Ignore(e => e.Password);

            entity.Property(e => e.EncryptedPassword)
                .HasColumnName("encrypted_password")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Language)
                .HasColumnName("language")
                .HasConversion<short>();

            entity.Ignore(e => e.LoginType);

            entity.Property(e => e.LoginStatus)
                .HasColumnName("login_status")
                .HasConversion<short?>();

            entity.Property(e => e.LoginAt)
                .HasColumnName("login_at")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.SessionId)
                .HasColumnName("session_id")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.LogoutAt)
                .HasColumnName("logout_at")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.Note)
                .HasColumnName("note")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.KeyChangePwd)
                .HasColumnName("key_change_pwd")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.DtBlocked)
                .HasColumnName("dt_blocked")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.DtExpiredPwd)
                .HasColumnName("dt_expired_password")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.WrondPwdAttempts)
                .HasColumnName("wrong_pwd_attempts");

            AuditRegisterConfiguration.Configure(entity);

            /* => Postgres: definições para o campo 'datetime'
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();  
             */
        }
    }
}
