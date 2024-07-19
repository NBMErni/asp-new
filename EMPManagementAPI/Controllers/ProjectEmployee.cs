using EMPManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEmployee : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public ProjectEmployee(EMPManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        [HttpGet]
        public IActionResult GetAllProjectEmployee()
        {
            var projectEmployee = dbContext.ProjectEmployee.ToList();

            return Ok(projectEmployee);
        }
    }
}
