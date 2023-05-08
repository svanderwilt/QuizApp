using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;

        public TestRepository(DataContext context, IMapper mapper,
            IQuestionRepository questionRepository)
        {

            _questionRepository = questionRepository;
            _mapper = mapper;
            _context = context;

        }
        public async Task<TestDto> AddTestAsync(AppUser user)
        {
            // get random list of questions and create a new test for user

            Test newTest = new Test();
            newTest.TotalQuestionNo = 150;
            newTest.AppUserId = user.Id;
            newTest.Questions = await _questionRepository.GetNQuestions(150);
            foreach (var question in newTest.Questions)
            {

                question.Tests.Add(newTest);

            }
            Console.WriteLine("saving db changes");
            await _context.SaveChangesAsync();
            var testDto = new TestDto();
            _mapper.Map(newTest, testDto);
            return testDto;

        }

        public void DeleteTest(int userId, int testId)
        {
            // delete the test from db
            var test = _context.Tests.First(t => t.Id == testId && t.AppUserId == userId);

            _context.Tests.Remove(test);
        }

        public async Task<PagedList<TestSummaryDto>> GetAllTestsForUser(int userId, PaginationParams userParams)
        {
            var query = _context.Tests.AsQueryable();
            query = query.Where(t => t.AppUserId == userId)
                    .OrderByDescending(t => t.TestEndTime);

            return await PagedList<TestSummaryDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<TestSummaryDto>(_mapper.ConfigurationProvider),
                userParams.PageNumber,
                userParams.PageSize);

        }

        public async Task<TestDto> GetTest(int id)
        {
            return await _context.Tests
                    .Where(t => t.Id == id)
                    .ProjectTo<TestDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TestQuestion> AnswerQuestion(string answer, int testId, int questionId)
        {
            var question = await _context.TestQuestions
                .Include(tq => tq.Question)
                .Where(tq => tq.TestID == testId && tq.QuestionID == questionId)
                .SingleOrDefaultAsync();
            question.UserAnswer = answer;
            question.IsAnswered = true;
            question.IsCorrect = question.Question.Answer.Equals(answer);
            var test = _context.Tests.First(t => t.Id == testId);
            test.AnsweredNo++;
            if (question.IsCorrect)
            {
                test.Score++;


            }


            if (test.AnsweredNo == test.TotalQuestionNo)
            {
                test.Completed = true;
                test.TestEndTime = DateTime.UtcNow;
            }

            return question;
        }

    }
}