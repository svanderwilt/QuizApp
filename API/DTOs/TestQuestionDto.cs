using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TestQuestionDto
    {
        public int Id { get; set; }

        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsAnswered { get; set; }
        public QuestionDto Question { get; set; }
    }
}