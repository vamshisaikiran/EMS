using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Foreign key for Group
        public int GroupId { get; set; }
        public virtual Group UserGroup { get; set; }

        // Navigation properties (relationships to other tables/entities)
        public ICollection<Exam> CreatedExams { get; set; } = new List<Exam>();  // For teachers: exams they've created
        public ICollection<ExamEnrollment> EnrolledExams { get; set; } = new List<ExamEnrollment>();  // For students: exams they've enrolled in
        public ICollection<Certificate> AchievedCertificates { get; set; } = new List<Certificate>();  // Certificates the user (mostly students) has achieved
    }
}
