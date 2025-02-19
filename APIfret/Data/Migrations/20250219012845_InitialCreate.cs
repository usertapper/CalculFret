using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIfret.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Iles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Intitule = table.Column<string>(type: "TEXT", nullable: false),
                    Codezonetarifaire = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifsrevatuas",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Intitule = table.Column<string>(type: "TEXT", nullable: false),
                    Refrigere = table.Column<bool>(type: "INTEGER", nullable: false),
                    TauxPriseEnCharge = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TarifFrets",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Case = table.Column<string>(type: "TEXT", nullable: false),
                    Montant = table.Column<decimal>(type: "TEXT", nullable: false),
                    IleDepartId = table.Column<int>(type: "INTEGER", nullable: false),
                    IleArriveeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TarifFrets_Iles_IleArriveeId",
                        column: x => x.IleArriveeId,
                        principalTable: "Iles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarifFrets_Iles_IleDepartId",
                        column: x => x.IleDepartId,
                        principalTable: "Iles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarifFrets_IleArriveeId",
                table: "TarifFrets",
                column: "IleArriveeId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifFrets_IleDepartId",
                table: "TarifFrets",
                column: "IleDepartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarifFrets");

            migrationBuilder.DropTable(
                name: "Tarifsrevatuas");

            migrationBuilder.DropTable(
                name: "Iles");
        }
    }
}
