using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_FinalExam_Net6.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool IsManager { get; set; }

        public Department Department { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
