using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                columns: new[] { "PredmetId", "Naziv", "Sifra" },
                values: new object[,]
                {
                    { 1, "Matematika 1", "MAT101" },
                    { 2, "Osnove Programiranja", "INF101" },
                    { 3, "Matematika 2", "MAT202" },
                    { 4, "Osnove Organizacije", "ORG101" }
                });

            migrationBuilder.InsertData(
                table: "Delovi",
                columns: new[] { "DeoId", "Naziv", "PredmetId" },
                values: new object[,]
                {
                    { 1, "Pismeni deo", 1 },
                    { 2, "Usmeni deo", 1 },
                    { 3, "Prvi kolokvijum", 1 },
                    { 4, "Drugi kolokvijum", 1 },
                    { 5, "Pismeni deo", 2 },
                    { 6, "Usmeni deo", 2 },
                    { 7, "Prvi kolokvijum", 2 },
                    { 8, "Drugi kolokvijum", 2 },
                    { 9, "Pismeni deo", 3 },
                    { 10, "Usmeni deo", 3 },
                    { 11, "Prvi kolokvijum", 3 },
                    { 12, "Drugi kolokvijum", 3 },
                    { 13, "Pismeni deo", 4 },
                    { 14, "Usmeni deo", 4 },
                    { 15, "Prvi kolokvijum", 4 },
                    { 16, "Drugi kolokvijum", 4 }
                });

            migrationBuilder.InsertData(
                table: "Predavaci",
                columns: new[] { "PredavacId", "Email", "Ime", "KatedraId", "Password", "Prezime", "Username" },
                values: new object[,]
                {
                    { 1, "petar.petrovic@example.com", "Petar", 1, "password123", "Petrovic", "ppetrovic" },
                    { 2, "marko.markovic@example.com", "Marko", 2, "password123", "Markovic", "mmarkovic" },
                    { 3, "zarko.zarkovic@example.com", "Zarko", 2, "password123", "Zarkovic", "zzarkovic" },
                    { 4, "janko.jankovic@example.com", "Janko", 3, "password123", "Jankovic", "jjankovic" },
                    { 5, "mirko.mirkovic@example.com", "Mirko", 1, "password123", "Mirkovic", "mmirkovic" }
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
