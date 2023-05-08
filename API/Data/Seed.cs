using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},

            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var user = new AppUser();
            user.UserName = "josh";
            user.KnownAs = "Josh";
            user.Created = DateTime.SpecifyKind(user.Created, DateTimeKind.Utc);
            user.LastActive = DateTime.UtcNow;
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            var user2 = new AppUser();
            user2.UserName = "scott";
            user2.KnownAs = "Scott";
            user2.Created = DateTime.UtcNow;
            await userManager.CreateAsync(user2, "Pa$$w0rd");
            await userManager.AddToRolesAsync(user2, new[] { "Member", "Admin" });


        }

        public static async Task SeedQuestions(DataContext context)
        {
            if (await context.Questions.AnyAsync()) return;
            var questionData = await File.ReadAllTextAsync("Data/DMVquestions.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            dynamic questions = JsonConvert.DeserializeObject(questionData);

            foreach (var question in questions)
            {
                var newQuestion = new Question();
                newQuestion.QuestionText = question.question;
                newQuestion.Choices = (question.choice).ToObject<List<string>>();
                newQuestion.Hint = question.hint;
                newQuestion.Answer = question.answer;
                newQuestion.Choices.Add(newQuestion.Answer);
                context.Questions.Add(newQuestion);
                await context.SaveChangesAsync();
            }
        }
    }
}