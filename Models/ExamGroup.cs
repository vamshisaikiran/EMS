namespace EMS.Models
{
    public class ExamGroup
    {
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}