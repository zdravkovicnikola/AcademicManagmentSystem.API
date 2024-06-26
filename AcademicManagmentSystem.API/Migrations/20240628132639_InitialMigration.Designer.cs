﻿// <auto-generated />
using System;
using AcademicManagmentSystem.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcademicManagmentSystem.API.Migrations
{
    [DbContext(typeof(AcademicManagmentSystemDbContext))]
    [Migration("20240628132639_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Deo", b =>
                {
                    b.Property<int>("DeoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeoId"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PredmetId")
                        .HasColumnType("int");

                    b.HasKey("DeoId");

                    b.HasIndex("PredmetId");

                    b.ToTable("Delovi");

                    b.HasData(
                        new
                        {
                            DeoId = 1,
                            Naziv = "Pismeni deo",
                            PredmetId = 1
                        },
                        new
                        {
                            DeoId = 2,
                            Naziv = "Usmeni deo",
                            PredmetId = 1
                        },
                        new
                        {
                            DeoId = 3,
                            Naziv = "Prvi kolokvijum",
                            PredmetId = 1
                        },
                        new
                        {
                            DeoId = 4,
                            Naziv = "Drugi kolokvijum",
                            PredmetId = 1
                        },
                        new
                        {
                            DeoId = 5,
                            Naziv = "Pismeni deo",
                            PredmetId = 2
                        },
                        new
                        {
                            DeoId = 6,
                            Naziv = "Usmeni deo",
                            PredmetId = 2
                        },
                        new
                        {
                            DeoId = 7,
                            Naziv = "Prvi kolokvijum",
                            PredmetId = 2
                        },
                        new
                        {
                            DeoId = 8,
                            Naziv = "Drugi kolokvijum",
                            PredmetId = 2
                        },
                        new
                        {
                            DeoId = 9,
                            Naziv = "Pismeni deo",
                            PredmetId = 3
                        },
                        new
                        {
                            DeoId = 10,
                            Naziv = "Usmeni deo",
                            PredmetId = 3
                        },
                        new
                        {
                            DeoId = 11,
                            Naziv = "Prvi kolokvijum",
                            PredmetId = 3
                        },
                        new
                        {
                            DeoId = 12,
                            Naziv = "Drugi kolokvijum",
                            PredmetId = 3
                        },
                        new
                        {
                            DeoId = 13,
                            Naziv = "Pismeni deo",
                            PredmetId = 4
                        },
                        new
                        {
                            DeoId = 14,
                            Naziv = "Usmeni deo",
                            PredmetId = 4
                        },
                        new
                        {
                            DeoId = 15,
                            Naziv = "Prvi kolokvijum",
                            PredmetId = 4
                        },
                        new
                        {
                            DeoId = 16,
                            Naziv = "Drugi kolokvijum",
                            PredmetId = 4
                        });
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Katedra", b =>
                {
                    b.Property<int>("KatedraID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KatedraID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KatedraID");

                    b.ToTable("Katedre");

                    b.HasData(
                        new
                        {
                            KatedraID = 1,
                            Naziv = "Katedra 1"
                        },
                        new
                        {
                            KatedraID = 2,
                            Naziv = "Katedra 2"
                        },
                        new
                        {
                            KatedraID = 3,
                            Naziv = "Katedra 3"
                        });
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Predavac", b =>
                {
                    b.Property<int>("PredavacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PredavacId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KatedraId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PredavacId");

                    b.HasIndex("KatedraId");

                    b.ToTable("Predavaci");

                    b.HasData(
                        new
                        {
                            PredavacId = 1,
                            Email = "petar.petrovic@example.com",
                            Ime = "Petar",
                            KatedraId = 1,
                            Password = "password123",
                            Prezime = "Petrovic",
                            Username = "ppetrovic"
                        },
                        new
                        {
                            PredavacId = 2,
                            Email = "marko.markovic@example.com",
                            Ime = "Marko",
                            KatedraId = 2,
                            Password = "password123",
                            Prezime = "Markovic",
                            Username = "mmarkovic"
                        },
                        new
                        {
                            PredavacId = 3,
                            Email = "zarko.zarkovic@example.com",
                            Ime = "Zarko",
                            KatedraId = 2,
                            Password = "password123",
                            Prezime = "Zarkovic",
                            Username = "zzarkovic"
                        },
                        new
                        {
                            PredavacId = 4,
                            Email = "janko.jankovic@example.com",
                            Ime = "Janko",
                            KatedraId = 3,
                            Password = "password123",
                            Prezime = "Jankovic",
                            Username = "jjankovic"
                        },
                        new
                        {
                            PredavacId = 5,
                            Email = "mirko.mirkovic@example.com",
                            Ime = "Mirko",
                            KatedraId = 1,
                            Password = "password123",
                            Prezime = "Mirkovic",
                            Username = "mmirkovic"
                        });
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Predmet", b =>
                {
                    b.Property<int>("PredmetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PredmetId"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PredmetId");

                    b.ToTable("Predmeti");

                    b.HasData(
                        new
                        {
                            PredmetId = 1,
                            Naziv = "Matematika 1",
                            Sifra = "MAT101"
                        },
                        new
                        {
                            PredmetId = 2,
                            Naziv = "Osnove Programiranja",
                            Sifra = "INF101"
                        },
                        new
                        {
                            PredmetId = 3,
                            Naziv = "Matematika 2",
                            Sifra = "MAT202"
                        },
                        new
                        {
                            PredmetId = 4,
                            Naziv = "Osnove Organizacije",
                            Sifra = "ORG101"
                        });
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.PredmetPredavac", b =>
                {
                    b.Property<int>("PredmetId")
                        .HasColumnType("int");

                    b.Property<int>("PredavacId")
                        .HasColumnType("int");

                    b.HasKey("PredmetId", "PredavacId");

                    b.HasIndex("PredavacId");

                    b.ToTable("PredmetPredavaci");

                    b.HasData(
                        new
                        {
                            PredmetId = 2,
                            PredavacId = 1
                        },
                        new
                        {
                            PredmetId = 1,
                            PredavacId = 2
                        },
                        new
                        {
                            PredmetId = 3,
                            PredavacId = 2
                        },
                        new
                        {
                            PredmetId = 1,
                            PredavacId = 3
                        },
                        new
                        {
                            PredmetId = 4,
                            PredavacId = 4
                        },
                        new
                        {
                            PredmetId = 2,
                            PredavacId = 5
                        });
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Rezultat", b =>
                {
                    b.Property<int>("RezultatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RezultatId"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeoId")
                        .HasColumnType("int");

                    b.Property<int>("Ocena")
                        .HasColumnType("int");

                    b.Property<double>("Poeni")
                        .HasColumnType("float");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("RezultatId");

                    b.HasIndex("DeoId");

                    b.HasIndex("StudentId");

                    b.ToTable("Rezultati");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Index")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Studenti");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Deo", b =>
                {
                    b.HasOne("AcademicManagmentSystem.API.Data.Predmet", "Predmet")
                        .WithMany("Delovi")
                        .HasForeignKey("PredmetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Predmet");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Predavac", b =>
                {
                    b.HasOne("AcademicManagmentSystem.API.Data.Katedra", "Katedra")
                        .WithMany("Predavaci")
                        .HasForeignKey("KatedraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Katedra");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.PredmetPredavac", b =>
                {
                    b.HasOne("AcademicManagmentSystem.API.Data.Predavac", "Predavac")
                        .WithMany("PredmetPredavaci")
                        .HasForeignKey("PredavacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicManagmentSystem.API.Data.Predmet", "Predmet")
                        .WithMany("PredmetPredavaci")
                        .HasForeignKey("PredmetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Predavac");

                    b.Navigation("Predmet");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Rezultat", b =>
                {
                    b.HasOne("AcademicManagmentSystem.API.Data.Deo", "Deo")
                        .WithMany("Rezultati")
                        .HasForeignKey("DeoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicManagmentSystem.API.Data.Student", "Student")
                        .WithMany("Rezultati")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deo");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Deo", b =>
                {
                    b.Navigation("Rezultati");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Katedra", b =>
                {
                    b.Navigation("Predavaci");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Predavac", b =>
                {
                    b.Navigation("PredmetPredavaci");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Predmet", b =>
                {
                    b.Navigation("Delovi");

                    b.Navigation("PredmetPredavaci");
                });

            modelBuilder.Entity("AcademicManagmentSystem.API.Data.Student", b =>
                {
                    b.Navigation("Rezultati");
                });
#pragma warning restore 612, 618
        }
    }
}
