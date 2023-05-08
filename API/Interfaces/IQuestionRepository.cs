using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        void AddQuestion(Question question);
        void DeleteQuestion(Question question);
        Task<ICollection<Question>> GetNQuestions(int n);
        Task<Question> GetQuestion(int id);
        Task<bool> SaveAllAsync();
    }
}