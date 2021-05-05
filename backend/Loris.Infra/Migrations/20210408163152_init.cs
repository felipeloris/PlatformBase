using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loris.Infra.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "auth_resource",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    dictionary = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_resource_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_role",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_role_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_id = table.Column<int>(type: "integer", nullable: true),
                    external_id = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    encrypted_password = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    nickname = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    language = table.Column<short>(type: "smallint", nullable: false),
                    login_status = table.Column<short>(type: "smallint", nullable: false),
                    login_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    note = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    token_change_pwd = table.Column<string>(type: "character varying(36)", unicode: false, maxLength: 36, nullable: true),
                    dt_blocked = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    dt_expired_password = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    wrong_pwd_attempts = table.Column<short>(type: "smallint", nullable: false),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_user_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courier_template",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<string>(type: "character varying(35)", unicode: false, maxLength: 35, nullable: false),
                    template_name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    template = table.Column<string>(type: "text", unicode: false, nullable: false),
                    system_sender_ident = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    system = table.Column<short>(type: "smallint", nullable: false),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courier_template_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_role_resource",
                schema: "public",
                columns: table => new
                {
                    auth_role_id = table.Column<int>(type: "integer", nullable: false),
                    auth_resource_id = table.Column<int>(type: "integer", nullable: false),
                    permissions = table.Column<short>(type: "smallint", nullable: false),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_role_resource_pk", x => new { x.auth_role_id, x.auth_resource_id });
                    table.ForeignKey(
                        name: "auth_role_resource_fk_auth_resource",
                        column: x => x.auth_resource_id,
                        principalSchema: "public",
                        principalTable: "auth_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "auth_role_resource_fk_auth_role",
                        column: x => x.auth_role_id,
                        principalSchema: "public",
                        principalTable: "auth_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "auth_user_role",
                schema: "public",
                columns: table => new
                {
                    auth_role_id = table.Column<int>(type: "integer", nullable: false),
                    auth_user_id = table.Column<int>(type: "integer", nullable: false),
                    ctrl_created_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_created_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    ctrl_modified_in = table.Column<long>(type: "bigint", nullable: true),
                    ctrl_modified_by = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_user_role_pk", x => new { x.auth_role_id, x.auth_user_id });
                    table.ForeignKey(
                        name: "auth_user_role_fk_auth_role",
                        column: x => x.auth_role_id,
                        principalSchema: "public",
                        principalTable: "auth_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "auth_user_role_fk_auth_user",
                        column: x => x.auth_user_id,
                        principalSchema: "public",
                        principalTable: "auth_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "courier_message",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action = table.Column<short>(type: "smallint", nullable: false),
                    generator = table.Column<short>(type: "smallint", nullable: false),
                    courier_template_id = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "text", unicode: false, nullable: false),
                    dt_inclusion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    from = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courier_message_pk", x => x.id);
                    table.ForeignKey(
                        name: "courier_message_fk_courier_template",
                        column: x => x.courier_template_id,
                        principalSchema: "public",
                        principalTable: "courier_template",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "courier_attachment",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    courier_message_id = table.Column<long>(type: "bigint", nullable: false),
                    file_name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    file_type = table.Column<short>(type: "smallint", nullable: false),
                    file = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courier_attachment_pk", x => x.id);
                    table.ForeignKey(
                        name: "courier_attachment_fk_courier_message",
                        column: x => x.courier_message_id,
                        principalSchema: "public",
                        principalTable: "courier_message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courier_to",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    courier_message_id = table.Column<long>(type: "bigint", nullable: false),
                    system_user_ident = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    system = table.Column<short>(type: "smallint", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    last_processing = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courier_to_pk", x => x.id);
                    table.ForeignKey(
                        name: "courier_to_fk_courier_message",
                        column: x => x.courier_message_id,
                        principalSchema: "public",
                        principalTable: "courier_message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "auth_resource_ix_code",
                schema: "public",
                table: "auth_resource",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_role_resource_auth_resource_id",
                schema: "public",
                table: "auth_role_resource",
                column: "auth_resource_id");

            migrationBuilder.CreateIndex(
                name: "auth_user_ix_email",
                schema: "public",
                table: "auth_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "auth_user_ix_external_id",
                schema: "public",
                table: "auth_user",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "auth_user_ix_nickname",
                schema: "public",
                table: "auth_user",
                column: "nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_user_role_auth_user_id",
                schema: "public",
                table: "auth_user_role",
                column: "auth_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_courier_attachment_courier_message_id",
                schema: "public",
                table: "courier_attachment",
                column: "courier_message_id");

            migrationBuilder.CreateIndex(
                name: "IX_courier_message_courier_template_id",
                schema: "public",
                table: "courier_message",
                column: "courier_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_courier_to_courier_message_id",
                schema: "public",
                table: "courier_to",
                column: "courier_message_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_role_resource",
                schema: "public");

            migrationBuilder.DropTable(
                name: "auth_user_role",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courier_attachment",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courier_to",
                schema: "public");

            migrationBuilder.DropTable(
                name: "auth_resource",
                schema: "public");

            migrationBuilder.DropTable(
                name: "auth_role",
                schema: "public");

            migrationBuilder.DropTable(
                name: "auth_user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courier_message",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courier_template",
                schema: "public");
        }
    }
}
