using Assignment_2_API.Data;
using Assignment_2_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmpManageDbContext _empmanageDbcontext;

        public DepartmentController(EmpManageDbContext empmanageDbcontext)
        {
            _empmanageDbcontext = empmanageDbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDept() //To Get List of All the department
        {
            var department = await _empmanageDbcontext.Departments.ToListAsync();
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDept(Department department) // To Create Department
        {
            await _empmanageDbcontext.Departments.AddAsync(department);
            await _empmanageDbcontext.SaveChangesAsync();

            return Ok(department);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDept(int id,Department department) // To Edit Department
        {
            //Using more optimized FirstOrDefaultAsync Method
            if(id != department.Id)
            {
                return BadRequest();
            }
            var DeptInList = await _empmanageDbcontext.Departments.FirstOrDefaultAsync(i => i.Id == department.Id);
            if (DeptInList == null)
            {
                return NotFound("Invalid ID");
            }
            DeptInList.DepartmentName = department.DepartmentName;

            await _empmanageDbcontext.SaveChangesAsync();
            return Ok(DeptInList);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteDept([FromRoute]int id) // To delete the department using id
        {
            //Using more optimized FirstOrDefaultAsync Method
            var department = await _empmanageDbcontext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if(department == null)
            {
                return NotFound();
            }
            _empmanageDbcontext.Departments.Remove(department);
            await _empmanageDbcontext.SaveChangesAsync();
            return Ok(department);
        }
    }
}
