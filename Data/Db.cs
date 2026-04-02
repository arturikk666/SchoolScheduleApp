using Microsoft.EntityFrameworkCore;
using _111.Models;

namespace _111.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Replacement> Replacements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolReplacements;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Teacher)
                .WithMany()
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Teacher)
                .WithMany()
                .HasForeignKey(a => a.TeacherId);

            modelBuilder.Entity<Replacement>()
                .HasOne(r => r.Absence)
                .WithMany()
                .HasForeignKey(r => r.AbsenceId);

            modelBuilder.Entity<Replacement>()
                .HasOne(r => r.SubstituteTeacher)
                .WithMany()
                .HasForeignKey(r => r.SubstituteTeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}