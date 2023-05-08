using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Choices { get; set; }
        public string Answer { get; set; }
        public string Hint { get; set; }
        public string PhotoUrl { get; set; } = "";
    }
}