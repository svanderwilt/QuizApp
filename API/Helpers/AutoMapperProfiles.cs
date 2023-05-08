
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Test, TestDto>();
            CreateMap<TestDto, Test>();
            CreateMap<Question, QuestionDto>();
            CreateMap<TestQuestion, TestQuestionDto>();
            CreateMap<Test, TestSummaryDto>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);

        }
    }
}