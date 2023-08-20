namespace EMS.Models;

public class ExamEnrollment
{
    public int Id { get; set; }

    public string? ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    public int ExamId { get; set; }
    public virtual Exam Exam { get; set; }

    public DateTime EnrollmentDate { get; set; }
    public int Score { get; set; }
    public DateTime? ExamDate { get; set; }
}
