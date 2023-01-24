namespace Ovning14_SkalProj.Models
{
#nullable disable
    public class ApplicationUserGymClass
    {
        public int ApplicationUserGymClassId { get; set; }
        // Foreign Keys
        public int GymClassId { get; set; }
        public int ApplicationUserId { get; set; }

        // Navigation properties
        public ICollection<ApplicationUserGymClass> AttendedClasses { get; set; }
        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; }


    }
}
