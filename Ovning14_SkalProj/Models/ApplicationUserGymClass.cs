namespace Ovning14_SkalProj.Models
{
    public class ApplicationUserGymClass
    {
        //public int ApplicationUserGymClassId { get; set; }
        // Foreign Keys
        public int GymClassId { get; set; }
        public int ApplicationUserId { get; set; }

        // Navigation properties
       

    }
}
