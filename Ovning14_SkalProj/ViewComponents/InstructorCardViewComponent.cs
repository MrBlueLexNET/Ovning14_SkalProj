using Microsoft.AspNetCore.Mvc;
using Ovning14_SkalProj.Core.Entities;

namespace Ovning14_SkalProj.ViewComponents
{
    public class InstructorCardViewComponent : ViewComponent
    {
      
        public IViewComponentResult Invoke(
            Instructor instructor)
        {
            return View(instructor);
        }
    }

}
