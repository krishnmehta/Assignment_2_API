using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_2_API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [Range(21, 100)]
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public double Salary { get; set; }


    }

}
