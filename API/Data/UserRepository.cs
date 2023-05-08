using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }



        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(t => t.Tests)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(t => t.Tests)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAnswer(int id, Question question, bool result)
        {
            var currUser = await _context.Users
                .Include(u => u.QuestionResults)
                .SingleOrDefaultAsync(u => u.Id == id);

            Console.WriteLine("Question is: " + question.QuestionText);
            Console.WriteLine("Result is: " + result);
            // get the questionresult for the user or create a new one if question hasn't been asked
            var questionResult = currUser.QuestionResults.FirstOrDefault(qr => qr.QuestionID == question.Id);

            if (questionResult == null)
            {
                questionResult = new QuestionResult();
                currUser.QuestionResults.Add(questionResult);
            }
            Console.WriteLine("Question has been asked " + questionResult.TimesAsked + " times.");
            questionResult.TimesAsked++;
            questionResult.QuestionID = question.Id;
            // if answered correctly update questionresult appropriately;
            if (result)
            {
                questionResult.TimesCorrectConsecutively++;
                if (questionResult.TimesCorrectConsecutively > 2) questionResult.Active = false;
            }
            else
            {
                questionResult.TimesCorrectConsecutively = 0;
                questionResult.TimesIncorrect++;
                questionResult.Active = true;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}