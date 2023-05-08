using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;


namespace API.Interfaces
{
    public interface ITestRepository
    {
        Task<TestDto> AddTestAsync(AppUser user);
        void DeleteTest(int userId, int testId);
        Task<TestDto> GetTest(int id);
        Task<PagedList<TestSummaryDto>> GetAllTestsForUser(int userId, PaginationParams userParams);
        Task<bool> SaveAllAsync();
        Task<TestQuestion> AnswerQuestion(string answer, int testId, int questionId);


    }
}