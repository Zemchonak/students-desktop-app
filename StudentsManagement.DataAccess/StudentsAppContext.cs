using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess
{
    public class StudentsAppContext : DbContext
    {
        public StudentsAppContext(DbContextOptions<StudentsAppContext> options)
            : base(options)
        { }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<CurriculumUnit> CurriculumUnits { get; set; }

        public DbSet<Attestation> Attestations { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<Mark> RetakeResult { get; set; }
    }
}
