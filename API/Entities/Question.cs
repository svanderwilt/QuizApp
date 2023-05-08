
namespace API.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Choices { get; set; }
        public string Answer { get; set; }
        public string Hint { get; set; }
        public string PhotoUrl { get; set; } = "";
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
}