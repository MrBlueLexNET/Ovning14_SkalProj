namespace Ovning14_SkalProj.Models
{
    public class GymClass
    {
        public int GymClassId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        //public DateTime EndTime { get { return StartTime + Duration; } }
        public DateTime EndTime => StartTime + Duration;
        string Description { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; } = new List<ApplicationUserGymClass>();

    }
}
