using Microsoft.EntityFrameworkCore;
using StudentsManagement.Common.Enums;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess
{
    public class StudentsAppContext : DbContext
    {
        public StudentsAppContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public StudentsAppContext(DbContextOptionsBuilder optionsBulder)
            : base(optionsBulder.Options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speciality>(
                //s => { s.HasKey(p => p.FacultyId); s.ToTable("Faculties")
                )
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<WorkType>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Subject>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<User>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Group>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<CurriculumUnit>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Attestation>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<Mark>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);
            modelBuilder.Entity<RetakeResult>()
                .Property(e => e.Id).HasDefaultValueSql(Constants.AutoGuidModelBuilderValue);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("0CAF4590-5AE3-4278-93CD-D4A7515065E4"),
                    Email = Constants.AdminEmail,
                    PasswordHash = Constants.AdminPasswordHash,
                    FirstName = Constants.AdminFirstName,
                    LastName = Constants.AdminLastName,
                    Role = UserRole.MainAdmin
                });
        }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<CurriculumUnit> CurriculumUnits { get; set; }

        public DbSet<Attestation> Attestations { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<RetakeResult> RetakeResult { get; set; }
    }
}
