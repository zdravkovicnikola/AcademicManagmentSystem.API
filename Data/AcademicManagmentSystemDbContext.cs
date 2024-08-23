using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Data
{
    public class AcademicManagmentSystemDbContext : DbContext
    {
        public AcademicManagmentSystemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Katedra> Katedre { get; set; }
        public DbSet<Predavac> Predavaci { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Deo> Delovi { get; set; }
        public DbSet<PredmetPredavac> PredmetPredavaci { get; set; }
        public DbSet<Ocena> Ocene { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definisemo odnos izmedju klase Katedra i klase Predavac
            modelBuilder.Entity<Katedra>()
                .HasMany(k => k.Predavaci)
                .WithOne(p => p.Katedra)
                .HasForeignKey(p => p.KatedraId);

            // Konfiguracija za relaciju više-više između klase Predmet i klase Predavac
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
                .HasForeignKey(d => d.PredmetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Definisanje odnosa između Student i Ocena
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Ocene)
                .WithOne(o => o.Student)
                .HasForeignKey(o => o.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Definisanje odnosa između Predmet i Ocena
            modelBuilder.Entity<Predmet>()
                .HasMany(p => p.Ocene)
                .WithOne(o => o.Predmet)
                .HasForeignKey(o => o.PredmetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Definisanje odnosa između Student i Deo
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Delovi)
                .WithOne(d => d.Student)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Definisanje odnosa između Student i Deo
            modelBuilder.Entity<Tip>()
                .HasMany<Deo>()
                .WithOne(d => d.Tip)
                .HasForeignKey(d => d.TipId)
                .OnDelete(DeleteBehavior.NoAction);


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
                    Password = "P@ssword_1",
                    KatedraId = 1
                },
                new Predavac
                {
                    PredavacId = 2,
                    Ime = "Marko",
                    Prezime = "Markovic",
                    Username = "mmarkovic",
                    Email = "marko.markovic@example.com",
                    Password = "P@ssword_1",
                    KatedraId = 2
                },
                new Predavac
                {
                    PredavacId = 3,
                    Ime = "Zarko",
                    Prezime = "Zarkovic",
                    Username = "zzarkovic",
                    Email = "zarko.zarkovic@example.com",
                    Password = "P@ssword_1",
                    KatedraId = 2
                },
                new Predavac
                {
                    PredavacId = 4,
                    Ime = "Janko",
                    Prezime = "Jankovic",
                    Username = "jjankovic",
                    Email = "janko.jankovic@example.com",
                    Password = "P@ssword_1",
                    KatedraId = 3
                },
                new Predavac
                {
                    PredavacId = 5,
                    Ime = "Mirko",
                    Prezime = "Mirkovic",
                    Username = "mmirkovic",
                    Email = "mirko.mirkovic@example.com",
                    Password = "P@ssword_1",
                    KatedraId = 1
                }
            );

            // Seed podaci za Predmet
            modelBuilder.Entity<Predmet>().HasData(
                new Predmet { PredmetId = 1, Naziv = "Matematika 1", Sifra = "MAT101", ESPB = 6 },
                new Predmet { PredmetId = 2, Naziv = "Osnove Programiranja", Sifra = "INF101", ESPB = 8 },
                new Predmet { PredmetId = 3, Naziv = "Matematika 2", Sifra = "MAT202", ESPB = 6 },
                new Predmet { PredmetId = 4, Naziv = "Osnove Organizacije", Sifra = "ORG101", ESPB = 5 }
            );

            // Seed podaci za PredmetPredavaci
            modelBuilder.Entity<PredmetPredavac>().HasData(
                new PredmetPredavac { PredmetId = 2, PredavacId = 1 },
                new PredmetPredavac { PredmetId = 1, PredavacId = 2 },
                new PredmetPredavac { PredmetId = 3, PredavacId = 2 },
                new PredmetPredavac { PredmetId = 1, PredavacId = 3 },
                new PredmetPredavac { PredmetId = 4, PredavacId = 4 },
                new PredmetPredavac { PredmetId = 2, PredavacId = 5 }
            );
        }
    }
}
