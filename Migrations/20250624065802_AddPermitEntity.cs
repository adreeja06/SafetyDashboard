using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1stModule_PIPremises.Migrations
{
    /// <inheritdoc />
    public partial class AddPermitEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PermitNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PermitType = table.Column<string>(type: "TEXT", nullable: false),
                    IssueDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FunctionalLocation = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StationName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permits");
        }
    }
}
