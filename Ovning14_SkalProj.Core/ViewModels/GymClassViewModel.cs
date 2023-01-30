using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovning14_SkalProj.Core.ViewModels
{
    public class GymClassViewModel
    {
        public int GymClassId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool Attending { get; set; }
    }
}
