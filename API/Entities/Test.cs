
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Entities
{
    [Table("Tests")]
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public DateTime TestStartTime { get; set; } = DateTime.UtcNow;
        public DateTime? TestEndTime { get; set; }
        public int Score { get; set; } = 0;
        public int AnsweredNo { get; set; } = 0;
        public int TotalQuestionNo { get; set; }
        public bool Completed { get; set; } = false;
        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    }
}