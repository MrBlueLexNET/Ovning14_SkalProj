using System.ComponentModel.DataAnnotations;


namespace Ovning14_SkalProj.Core.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string  LastName { get; set; } = string.Empty;
        public bool IsPersonalTrainer { get; set; } = false;
        public string Biography { get; set; } = string.Empty;
        [Timestamp]
        public byte[] RowVersion { get; set; } //= new byte[8];
    }
}
