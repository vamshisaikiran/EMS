namespace EMS.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerOptions { get; set; }
        public string CorrectAnswer { get; set; }
        public int Score { get; set; } // Score for the particular question

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}