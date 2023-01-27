using Ovning14_SkalProj.Core.Entities;

namespace Ovning14_SkalProj.Models.ViewModels
{
    public class InstructorIndexViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<GymClass> GymClasses { get; set; }
        public IEnumerable<ApplicationUserGymClass> AppUsersGymClasses { get; set; }
    }
}
