using EMPManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Project : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public Project(EMPManagementDbContext dbContext)
        {
          this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProject()
        {
            var projects = dbContext.Project.ToList();

            return Ok(projects);
        }
    }
}
