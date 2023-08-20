using System.Collections.Generic;
namespace EMS.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }

        // Refers to ApplicationUser who created the exam
        public string TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        // Information regarding score
        public int PassingScore { get; set; }
        public int TotalScore { get; set; } // The maximum achievable score for the exam

        // Navigation properties
        public virtual ICollection<Question> Questions { get; set; } // Questions for the exam
        public virtual ICollection<ExamEnrollment> StudentsEnrolled { get; set; } // Students enrolled in this exam

        // This entity represents which groups have access to this exam
        // If each exam can belong to multiple groups and vice versa, then you need this. 
        // Otherwise, you can simplify it with just a foreign key to a single Group.
        public virtual ICollection<ExamGroup> ExamGroups { get; set; }
    }
}
