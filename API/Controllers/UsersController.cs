using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;

        public UsersController(IUserRepository userRepository, IMapper mapper, ITestRepository testRepository)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _userRepository = userRepository;

        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }


        [HttpGet("{username}/all-tests")]
        public async Task<ActionResult<ICollection<TestDto>>> GetAllTestsForUser(string username,
                            [FromQuery] PaginationParams paginationParams)
        {
            var user = await _userRepository.GetMemberAsync(username);
            var tests = await _testRepository.GetAllTestsForUser(user.Id, paginationParams);
            Response.AddPaginationHeader(new PaginationHeader(tests.CurrentPage,
                    tests.PageSize, tests.TotalCount, tests.TotalPages));
            return Ok(tests);
        }

        [HttpGet("{username}/{testId}")]
        public async Task<ActionResult<TestDto>> GetTest(string username, int testId)
        {
            if (User.GetUsername() == username)
            {
                var test = await _testRepository.GetTest(testId);
                if (test == null) return NotFound();

                return Ok(test);
            }
            return BadRequest("Unauthorized access");

        }

        [HttpDelete("delete-test/{testId}")]
        public async Task<ActionResult> DeleteTest(int testId)
        {
            _testRepository.DeleteTest(User.GetUserId(), testId);
            if (await _testRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting test");
        }

    }
}