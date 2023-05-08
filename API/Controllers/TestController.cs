using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;
        public TestController(IUserRepository userRepository, ITestRepository testRepository)
        {
            _testRepository = testRepository;
            _userRepository = userRepository;

        }
        [HttpGet]
        public async Task<ActionResult<TestDto>> GetNewTest()
        {
            Console.WriteLine("Trying to create a test");
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            return await _testRepository.AddTestAsync(user);
        }

        [HttpGet("{testId}")]
        public async Task<ActionResult<TestDto>> GetTest(int testId)
        {
            return await _testRepository.GetTest(testId);

        }

        [HttpPost("{testId}/{questionId}")]
        public async Task<ActionResult<TestQuestion>> AnswerQuestion(int testId, int questionId, [FromBody] AnswerDto answer)
        {
            var question = await _testRepository.AnswerQuestion(answer.Answer, testId, questionId);

            if (await _testRepository.SaveAllAsync())
            {
                var questionResult = await _userRepository.AddAnswer(User.GetUserId(), question.Question, question.IsCorrect);


                if (questionResult) return Ok(question);

                return BadRequest("Failed to save questionResult");
            }
            else return BadRequest("Failed to save answer");
        }
    }
}