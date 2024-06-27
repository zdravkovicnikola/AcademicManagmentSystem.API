using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicManagmentSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Katedre",
                columns: table => new
                {
                    KatedraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katedre", x => x.KatedraID);
                });

            migrationBuilder.CreateTable(
                name: "Predmeti",
                columns: table => new
                {
                    PredmetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmeti", x => x.PredmetId);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Predavaci",
                columns: table => new
                {
                    PredavacId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KatedraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predavaci", x => x.PredavacId);
                    table.ForeignKey(
                        name: "FK_Predavaci_Katedre_KatedraId",
                        column: x => x.KatedraId,
                        principalTable: "Katedre",
                        principalColumn: "KatedraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delovi",
                columns: table => new
                {
                    DeoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delovi", x => x.DeoId);
                    table.ForeignKey(
                        name: "FK_Delovi_Predmeti_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PredmetPredavaci",
                columns: table => new
                {
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    PredavacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredmetPredavaci", x => new { x.PredmetId, x.PredavacId });
                    table.ForeignKey(
                        name: "FK_PredmetPredavaci_Predavaci_PredavacId",
                        column: x => x.PredavacId,
                        principalTable: "Predavaci",
                        principalColumn: "PredavacId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PredmetPredavaci_Predmeti_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezultati",
                columns: table => new
                {
                    RezultatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poeni = table.Column<double>(type: "float", nullable: false),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeoId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezultati", x => x.RezultatId);
                    table.ForeignKey(
                        name: "FK_Rezultati_Delovi_DeoId",
                        column: x => x.DeoId,
                        principalTable: "Delovi",
                        principalColumn: "DeoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezultati_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delovi_PredmetId",
                table: "Delovi",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Predavaci_KatedraId",
                table: "Predavaci",
                column: "KatedraId");

            migrationBuilder.CreateIndex(
                name: "IX_PredmetPredavaci_PredavacId",
                table: "PredmetPredavaci",
                column: "PredavacId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_DeoId",
                table: "Rezultati",
                column: "DeoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_StudentId",
                table: "Rezultati",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredmetPredavaci");

            migrationBuilder.DropTable(
                name: "Rezultati");

            migrationBuilder.DropTable(
                name: "Predavaci");

            migrationBuilder.DropTable(
                name: "Delovi");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Katedre");

            migrationBuilder.DropTable(
                name: "Predmeti");
        }
    }
}
