namespace Ovning14_SkalProj.Models
{
#nullable disable
    public class GymClass
    {
        public int GymClassId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }
        string Description { get; set; }

        // Navigation properties
        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; }
       
    }
}
