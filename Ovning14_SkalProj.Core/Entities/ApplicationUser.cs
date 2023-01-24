using Microsoft.AspNetCore.Identity;

namespace Ovning14_SkalProj.Models
{

    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> AttendedClasses { get; set; } = new List<ApplicationUserGymClass>();   

    }
}
