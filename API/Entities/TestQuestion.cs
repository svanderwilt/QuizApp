

namespace API.Entities
{
    public class TestQuestion
    {
        public int QuestionID { get; set; }
        public int TestID { get; set; }

        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsAnswered { get; set; } = false;
        public Test Test { get; set; }
        public Question Question { get; set; }
    }
}