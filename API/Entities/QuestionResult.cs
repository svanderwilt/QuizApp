

namespace API.Entities
{
    public class QuestionResult
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }

        public int TimesAsked { get; set; } = 0;
        public int TimesIncorrect { get; set; } = 0;
        public int TimesCorrectConsecutively { get; set; } = 0;
        public bool Active { get; set; } = true;
    }
}