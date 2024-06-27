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
        }
    }
    
}

