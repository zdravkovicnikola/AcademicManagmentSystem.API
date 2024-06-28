using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AcademicManagmentSystem.API.Data
{
    public class AcademicManagmentSystemDbContext : DbContext
    {
        public AcademicManagmentSystemDbContext(DbContextOptions options) : base(options) { 
        
        
            }

        public DbSet<Katedra> Katedre { get; set; }
        public DbSet<Predavac> Predavaci { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<Deo> Delovi { get; set; }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<PredmetPredavac> PredmetPredavaci { get; set; }
        public DbSet<Rezultat> Rezultati { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Definisemo odnos izmedju klase Katedra i klase Predavac
            modelBuilder.Entity<Katedra>()
                .HasMany(k => k.Predavaci)
                .WithOne(p => p.Katedra)
                .HasForeignKey(p => p.KatedraId);

            //Kofiguracija za relaciju vise-vise izmedju klase Predmet i klase Predavac
            modelBuilder.Entity<PredmetPredavac>()
                .HasKey(pp => new { pp.PredmetId, pp.PredavacId });

            modelBuilder.Entity<PredmetPredavac>()
                .HasOne(pp => pp.Predmet)
                .WithMany(p => p.PredmetPredavaci)
                .HasForeignKey(pp => pp.PredmetId);

            modelBuilder.Entity<PredmetPredavac>()
                .HasOne(pp => pp.Predavac)
                .WithMany(p => p.PredmetPredavaci)
                .HasForeignKey(pp => pp.PredavacId);

            // Definisanje odnosa između Predmet i Deo
            modelBuilder.Entity<Predmet>()
                .HasMany(p => p.Delovi)
                .WithOne(d => d.Predmet)
                .HasForeignKey(d => d.PredmetId);

            // Definisanje odnosa između Deo i Rezultat
            modelBuilder.Entity<Deo>()
                .HasMany(d => d.Rezultati)
                .WithOne(r => r.Deo)
                .HasForeignKey(r => r.DeoId);

            // Definisanje odnosa između Student i Rezultat
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Rezultati)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId);

            //Seedovanje podataka

            // Seed podaci za Katedra
            modelBuilder.Entity<Katedra>().HasData(
                new Katedra { KatedraID = 1, Naziv = "Katedra 1" },
                new Katedra { KatedraID = 2, Naziv = "Katedra 2" },
                new Katedra { KatedraID = 3, Naziv = "Katedra 3" }
            );

            // Seed podaci za Predavac
            modelBuilder.Entity<Predavac>().HasData(
                new Predavac
                {
                    PredavacId = 1,
                    Ime = "Petar",
                    Prezime = "Petrovic",
                    Username = "ppetrovic",
                    Email = "petar.petrovic@example.com",
                    Password = "password123",
                    KatedraId = 1
                },
                new Predavac
                {
                    PredavacId = 2,
                    Ime = "Marko",
                    Prezime = "Markovic",
                    Username = "mmarkovic",
                    Email = "marko.markovic@example.com",
                    Password = "password123",
                    KatedraId = 2
                },
                new Predavac
                {
                    PredavacId = 3,
                    Ime = "Zarko",
                    Prezime = "Zarkovic",
                    Username = "zzarkovic",
                    Email = "zarko.zarkovic@example.com",
                    Password = "password123",
                    KatedraId = 2
                },
                new Predavac
                {
                    PredavacId = 4,
                    Ime = "Janko",
                    Prezime = "Jankovic",
                    Username = "jjankovic",
                    Email = "janko.jankovic@example.com",
                    Password = "password123",
                    KatedraId = 3
                },
                new Predavac
                {
                    PredavacId = 5,
                    Ime = "Mirko",
                    Prezime = "Mirkovic",
                    Username = "mmirkovic",
                    Email = "mirko.mirkovic@example.com",
                    Password = "password123",
                    KatedraId = 1
                }
);

            // Seed podaci za PredmetPredavaci
            modelBuilder.Entity<PredmetPredavac>().HasData(
                new PredmetPredavac { PredavacId = 1, PredmetId = 2 },
                new PredmetPredavac { PredavacId = 2, PredmetId = 1 },
                new PredmetPredavac { PredavacId = 2, PredmetId = 3 },
                new PredmetPredavac { PredavacId = 3, PredmetId = 1 },
                new PredmetPredavac { PredavacId = 4, PredmetId = 4 },
                new PredmetPredavac { PredavacId = 5, PredmetId = 2 }
            );

            // Seed podaci za Predmet
            modelBuilder.Entity<Predmet>().HasData(
                new Predmet { PredmetId = 1, Naziv = "Matematika 1", Sifra = "MAT101" },
                new Predmet { PredmetId = 2, Naziv = "Osnove Programiranja", Sifra = "INF101" },
                new Predmet { PredmetId = 3, Naziv = "Matematika 2", Sifra = "MAT202" },
                new Predmet { PredmetId = 4, Naziv = "Osnove Organizacije", Sifra = "ORG101" }
            );


            // Seed podaci za Deo
            modelBuilder.Entity<Deo>().HasData(
                new Deo { DeoId = 1, Naziv = "Pismeni deo", PredmetId = 1 },
                new Deo { DeoId = 2, Naziv = "Usmeni deo", PredmetId = 1 },
                new Deo { DeoId = 3, Naziv = "Prvi kolokvijum", PredmetId = 1 },
                new Deo { DeoId = 4, Naziv = "Drugi kolokvijum", PredmetId = 1 },

                new Deo { DeoId = 5, Naziv = "Pismeni deo", PredmetId = 2 },
                new Deo { DeoId = 6, Naziv = "Usmeni deo", PredmetId = 2 },
                new Deo { DeoId = 7, Naziv = "Prvi kolokvijum", PredmetId = 2 },
                new Deo { DeoId = 8, Naziv = "Drugi kolokvijum", PredmetId = 2 },

                new Deo { DeoId = 9, Naziv = "Pismeni deo", PredmetId = 3 },
                new Deo { DeoId = 10, Naziv = "Usmeni deo", PredmetId = 3 },
                new Deo { DeoId = 11, Naziv = "Prvi kolokvijum", PredmetId = 3 },
                new Deo { DeoId = 12, Naziv = "Drugi kolokvijum", PredmetId = 3 },

                new Deo { DeoId = 13, Naziv = "Pismeni deo", PredmetId = 4 },
                new Deo { DeoId = 14, Naziv = "Usmeni deo", PredmetId = 4 },
                new Deo { DeoId = 15, Naziv = "Prvi kolokvijum", PredmetId = 4 },
                new Deo { DeoId = 16, Naziv = "Drugi kolokvijum", PredmetId = 4 }

            );

        }
    }
    
}

