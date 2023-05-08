using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public List<Test> Tests { get; set; } = new List<Test>();
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<QuestionResult> QuestionResults { get; set; }

    }
}