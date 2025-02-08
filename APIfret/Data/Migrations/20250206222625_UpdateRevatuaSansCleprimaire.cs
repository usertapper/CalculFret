using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIfret.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRevatuaSansCleprimaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarifsrevatuas",
                table: "Tarifsrevatuas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tarifsrevatuas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tarifsrevatuas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarifsrevatuas",
                table: "Tarifsrevatuas",
                column: "Id");
        }
    }
}
