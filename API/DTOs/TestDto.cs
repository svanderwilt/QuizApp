using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class TestDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DateTime TestStartTime { get; set; }
        public DateTime? TestEndTime { get; set; }
        public int Score { get; set; }
        public int AnsweredNo { get; set; }
        public int TotalQuestionNo { get; set; }
        public bool Completed { get; set; }
        public ICollection<TestQuestionDto> TestQuestions { get; set; }
    }
}