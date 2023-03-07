using System.ComponentModel.DataAnnotations;

namespace Assignment_2_API.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string DepartmentName { get; set; }
    }
}