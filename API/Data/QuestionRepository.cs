using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public QuestionRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public void AddQuestion(Question question)
        {
            _context.Questions.Add(question);
        }

        public void DeleteQuestion(Question question)
        {
            _context.Questions.Remove(question);
        }

        public async Task<ICollection<Question>> GetNQuestions(int questionCount)
        {
            return await _context.Questions
                        .FromSql($"SELECT * FROM \"Questions\" ORDER BY random() ")
                        .Take(questionCount)

                        .ToListAsync();

        }

        public async Task<Question> GetQuestion(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}