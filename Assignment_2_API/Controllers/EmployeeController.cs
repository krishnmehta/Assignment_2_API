using Assignment_2_API.Data;
using Assignment_2_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmpManageDbContext _empmanageDbContext;

        public EmployeeController(EmpManageDbContext empManageDbContext)
        {
            _empmanageDbContext = empManageDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee() // To Get the List of All Employess
        {
            var employee = await _empmanageDbContext.Employees.Include(i => i.Department).ToListAsync();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute]int id) //To get a particular employee based on the given id
        {
            var employee = await _empmanageDbContext.Employees.Include(i => i.Department).FirstOrDefaultAsync(x =>x.Id == id);
            if(employee == null)
            {
                return NotFound("Employee Not Found");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee) // To Create Employee
        {
            //Checking if the entered employee department exist or not
            var department = await _empmanageDbContext.Departments.FirstOrDefaultAsync(i => i.Id == employee.DepartmentId);
            if(department == null)
            {
                return NotFound("Depatment doesn't exist"); // If department does not exist  return
            }
            employee.Department = department;
            await _empmanageDbContext.Employees.AddAsync(employee); // If dept exist Add Employee
            await _empmanageDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee([FromRoute]int id,Employee employee) //To Edit the employee
        {
            if (id != employee.Id) // Checking if the given employee id exist or not
            {
                return BadRequest();
            }

            var EmployeeInList = await _empmanageDbContext.Employees.FirstOrDefaultAsync(i => i.Id == employee.Id);

            var department = await _empmanageDbContext.Departments.FirstOrDefaultAsync(i => i.Id == employee.DepartmentId);
            if(department == null) //Checking if the updated department id exist or not
            {
                return NotFound("Depatment doesn't exist");
            }
            EmployeeInList.Name = employee.Name;
            EmployeeInList.Age= employee.Age;
            EmployeeInList.DepartmentId= employee.DepartmentId;
            EmployeeInList.Salary = employee.Salary;

            await _empmanageDbContext.SaveChangesAsync(); // If everything is perfect saving changes
            return Ok(EmployeeInList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute]int id) //To delete employee based on given id
        {
            var employee = await _empmanageDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) //Checking if the given employee id exist or not
            {
                return NotFound();
            }
            _empmanageDbContext.Employees.Remove(employee); //Removing the employee
            await _empmanageDbContext.SaveChangesAsync(); //Saving the changes
            return Ok(employee);
        }

    }
}
