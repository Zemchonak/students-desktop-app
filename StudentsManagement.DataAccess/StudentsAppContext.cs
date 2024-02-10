using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess
{
    public class StudentsAppContext : DbContext
    {
        public StudentsAppContext(DbContextOptions<StudentsAppContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Speciality>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Faculty>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Discipline>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<User>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Group>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<CurriculumUnit>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Attestation>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Mark>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<RetakeResult>().Property(e => e.Id)
                .HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
        }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<CurriculumUnit> CurriculumUnits { get; set; }

        public DbSet<Attestation> Attestations { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<RetakeResult> RetakeResult { get; set; }
    }
}
