using System.ComponentModel.DataAnnotations;

namespace ASP_FinalExam_Net6.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue - 1)]
        public int EmployeeCount { get; set; }
    }
}
