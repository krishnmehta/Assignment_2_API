using Assignment_2_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
