using Microsoft.AspNetCore.Identity;

namespace Ovning14_SkalProj.Models
{
#nullable disable
    public class ApplicationUser : IdentityUser
    {
        public int ApplicationUserId { get; set; }
        //Nav prop
        public ICollection<ApplicationUserGymClass> AttendedClasses { get; set; }

    }
}
