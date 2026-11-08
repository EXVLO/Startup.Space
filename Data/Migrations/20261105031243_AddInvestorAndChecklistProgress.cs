using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StartupLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestorAndChecklistProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_checklist_asp_net_users_user_id",
                table: "user_checklist");

            migrationBuilder.DropForeignKey(
                name: "fk_user_checklist_toolkit_checklist_toolkit_checklist_id",
                table: "user_checklist");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_checklist",
                table: "user_checklist");

            migrationBuilder.RenameTable(
                name: "user_checklist",
                newName: "toolkit_checklist_progress");

            migrationBuilder.RenameIndex(
                name: "ix_user_checklist_toolkit_checklist_id",
                table: "toolkit_checklist_progress",
                newName: "ix_toolkit_checklist_progress_toolkit_checklist_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_toolkit_checklist_progress",
                table: "toolkit_checklist_progress",
                columns: new[] { "user_id", "toolkit_checklist_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_toolkit_checklist_progress_toolkit_checklists_toolkit_check",
                table: "toolkit_checklist_progress",
                column: "toolkit_checklist_id",
                principalTable: "toolkit_checklist",
                principalColumn: "toolkit_checklist_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_toolkit_checklist_progress_users_user_id",
                table: "toolkit_checklist_progress",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "address_key",
                table: "toolkit_glossary");

            migrationBuilder.RenameColumn(
                name: "address_key",
                table: "toolkit_document",
                newName: "url");

            migrationBuilder.AddColumn<bool>(
                name: "is_investor",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "startup_checklist",
                columns: table => new
                {
                    startup_checklist_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "text", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_startup_checklist", x => x.startup_checklist_id);
                });

            migrationBuilder.CreateTable(
                name: "startup_checklist_progress",
                columns: table => new
                {
                    startup_id = table.Column<int>(type: "integer", nullable: false),
                    startup_checklist_id = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    date_completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_startup_checklist_progress", x => new { x.startup_id, x.startup_checklist_id });
                    table.ForeignKey(
                        name: "fk_startup_checklist_progress_startup_checklists_startup_check",
                        column: x => x.startup_checklist_id,
                        principalTable: "startup_checklist",
                        principalColumn: "startup_checklist_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_startup_checklist_progress_startups_startup_id",
                        column: x => x.startup_id,
                        principalTable: "startup",
                        principalColumn: "startup_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_startup_checklist_progress_startup_checklist_id",
                table: "startup_checklist_progress",
                column: "startup_checklist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "startup_checklist_progress");

            migrationBuilder.DropTable(
                name: "startup_checklist");

            migrationBuilder.DropColumn(
                name: "is_investor",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "toolkit_document",
                newName: "address_key");

            migrationBuilder.AddColumn<string>(
                name: "address_key",
                table: "toolkit_glossary",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropForeignKey(
                name: "fk_toolkit_checklist_progress_toolkit_checklists_toolkit_check",
                table: "toolkit_checklist_progress");

            migrationBuilder.DropForeignKey(
                name: "fk_toolkit_checklist_progress_users_user_id",
                table: "toolkit_checklist_progress");

            migrationBuilder.DropPrimaryKey(
                name: "pk_toolkit_checklist_progress",
                table: "toolkit_checklist_progress");

            migrationBuilder.RenameTable(
                name: "toolkit_checklist_progress",
                newName: "user_checklist");

            migrationBuilder.RenameIndex(
                name: "ix_toolkit_checklist_progress_toolkit_checklist_id",
                table: "user_checklist",
                newName: "ix_user_checklist_toolkit_checklist_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_checklist",
                table: "user_checklist",
                columns: new[] { "user_id", "toolkit_checklist_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_checklist_asp_net_users_user_id",
                table: "user_checklist",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_checklist_toolkit_checklist_toolkit_checklist_id",
                table: "user_checklist",
                column: "toolkit_checklist_id",
                principalTable: "toolkit_checklist",
                principalColumn: "toolkit_checklist_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
