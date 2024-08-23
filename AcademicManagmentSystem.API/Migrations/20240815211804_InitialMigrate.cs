using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademicManagmentSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
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
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ESPB = table.Column<int>(type: "int", nullable: false)
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
                name: "Tip",
                columns: table => new
                {
                    TipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tip", x => x.TipId);
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
                name: "Ocene",
                columns: table => new
                {
                    DatumPolaganja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrednostOcene = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocene", x => x.DatumPolaganja);
                    table.ForeignKey(
                        name: "FK_Ocene_Predmeti_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocene_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delovi",
                columns: table => new
                {
                    DeoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojPoena = table.Column<double>(type: "float", nullable: false),
                    MaxBrPoena = table.Column<double>(type: "float", nullable: false),
                    Polozio = table.Column<bool>(type: "bit", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TipId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Delovi_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delovi_Tip_TipId",
                        column: x => x.TipId,
                        principalTable: "Tip",
                        principalColumn: "TipId");
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

            migrationBuilder.InsertData(
                table: "Katedre",
                columns: new[] { "KatedraID", "Naziv" },
                values: new object[,]
                {
                    { 1, "Katedra 1" },
                    { 2, "Katedra 2" },
                    { 3, "Katedra 3" }
                });

            migrationBuilder.InsertData(
                table: "Predmeti",
                columns: new[] { "PredmetId", "ESPB", "Naziv", "Sifra" },
                values: new object[,]
                {
                    { 1, 6, "Matematika 1", "MAT101" },
                    { 2, 8, "Osnove Programiranja", "INF101" },
                    { 3, 6, "Matematika 2", "MAT202" },
                    { 4, 5, "Osnove Organizacije", "ORG101" }
                });

            migrationBuilder.InsertData(
                table: "Predavaci",
                columns: new[] { "PredavacId", "Email", "Ime", "KatedraId", "Password", "Prezime", "Username" },
                values: new object[,]
                {
                    { 1, "petar.petrovic@example.com", "Petar", 1, "P@ssword_1", "Petrovic", "ppetrovic" },
                    { 2, "marko.markovic@example.com", "Marko", 2, "P@ssword_1", "Markovic", "mmarkovic" },
                    { 3, "zarko.zarkovic@example.com", "Zarko", 2, "P@ssword_1", "Zarkovic", "zzarkovic" },
                    { 4, "janko.jankovic@example.com", "Janko", 3, "P@ssword_1", "Jankovic", "jjankovic" },
                    { 5, "mirko.mirkovic@example.com", "Mirko", 1, "P@ssword_1", "Mirkovic", "mmirkovic" }
                });

            migrationBuilder.InsertData(
                table: "PredmetPredavaci",
                columns: new[] { "PredavacId", "PredmetId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 5, 2 },
                    { 2, 3 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delovi_PredmetId",
                table: "Delovi",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Delovi_StudentId",
                table: "Delovi",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Delovi_TipId",
                table: "Delovi",
                column: "TipId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocene_PredmetId",
                table: "Ocene",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocene_StudentId",
                table: "Ocene",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Predavaci_KatedraId",
                table: "Predavaci",
                column: "KatedraId");

            migrationBuilder.CreateIndex(
                name: "IX_PredmetPredavaci_PredavacId",
                table: "PredmetPredavaci",
                column: "PredavacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delovi");

            migrationBuilder.DropTable(
                name: "Ocene");

            migrationBuilder.DropTable(
                name: "PredmetPredavaci");

            migrationBuilder.DropTable(
                name: "Tip");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Predavaci");

            migrationBuilder.DropTable(
                name: "Predmeti");

            migrationBuilder.DropTable(
                name: "Katedre");
        }
    }
}
