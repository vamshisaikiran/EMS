using EMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<ExamEnrollment> ExamEnrollments { get; set; }
        public DbSet<ExamGroup> ExamGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Question> Questions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=VAMSHI\\SQLEXPRESS;Database=EMSDB;Trusted_Connection=True;TrustServerCertificate=true");
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. One-to-Many between ApplicationUser (Teacher) and Exam
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CreatedExams)
                .WithOne(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId);

            // 2. Many-to-Many between ApplicationUser (Student) and Exam via ExamEnrollment
            modelBuilder.Entity<ExamEnrollment>()
                .HasOne(ee => ee.ApplicationUser)
                .WithMany(u => u.EnrolledExams)
                .HasForeignKey(ee => ee.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ExamEnrollment>()
                .HasOne(ee => ee.Exam)
                .WithMany(e => e.StudentsEnrolled)
                .HasForeignKey(ee => ee.ExamId)
                .OnDelete(DeleteBehavior.Restrict);


            // 3. One-to-Many between ApplicationUser and Certificate
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AchievedCertificates)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId);

            // 4. One-to-Many between Group and ApplicationUser
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithOne(u => u.UserGroup)
                .HasForeignKey(u => u.GroupId);

            // 5. Many-to-Many between Exam and Group via ExamGroup
            modelBuilder.Entity<ExamGroup>()
                .HasKey(eg => new { eg.ExamId, eg.GroupId });  // Composite key

            modelBuilder.Entity<ExamGroup>()
                .HasOne(eg => eg.Exam)
                .WithMany(e => e.ExamGroups)
                .HasForeignKey(eg => eg.ExamId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ExamGroup>()
                .HasOne(eg => eg.Group)
                .WithMany(g => g.ExamGroups)
                .HasForeignKey(eg => eg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);


            // 6. One-to-Many between Exam and Question
            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(q => q.ExamId);
        }

    }
}