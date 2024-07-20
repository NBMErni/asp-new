using Azure;
using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO.EmployeeInformation;
using EMPManagementAPI.Models.DTO.ProjectEmployee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public ProjectEmployeeController(EMPManagementDbContext dbContext)
        {
         
            this.dbContext = dbContext;
        }

        // GET: api/ProjectEmployee
        [HttpGet]
        public IActionResult GetAllProjectEmployees()
        {
            var projectEmployees = dbContext.ProjectEmployee.Include(e => e.Project).Include(e => e.EmployeeInformation).ToList();

            return Ok(projectEmployees);
        }

        // GET: api/ProjectEmployee/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeProjectById(int id)
        {
            var projectEmployees = dbContext.ProjectEmployee.Include(e => e.Project).Include(e => e.EmployeeInformation).FirstOrDefault(x => x.ProjectEmployeeId == id);

            if (projectEmployees == null)
            {
                return NotFound();
            }

            return Ok(projectEmployees);
        }

        // POST: api/ProjectEmployee
        [HttpPost]
        public IActionResult CreateProjectEmployee([FromBody] CreateProjectEmployeeDto createProjectemployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var projectEmployee = new ProjectEmployee
                {
                   EmployeeId = createProjectemployee.EmployeeId,
                   ProjectId = createProjectemployee.ProjectId
                };


                dbContext.ProjectEmployee.Add(projectEmployee);
                dbContext.SaveChanges();


                return CreatedAtAction(nameof(GetEmployeeProjectById), new { id = projectEmployee.ProjectEmployeeId }, projectEmployee);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        // PUT: api/ProjectEmployee/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateProjectEmployee(int id, [FromBody] UpdateProjectEmployeeDto updateProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectEmployee = dbContext.ProjectEmployee.FirstOrDefault(x => x.ProjectEmployeeId == id);

            if (projectEmployee == null)
            {
                return NotFound();
            }

            try
            {
                projectEmployee.EmployeeId = updateProjectDto.EmployeeId;
                projectEmployee.ProjectId = updateProjectDto.ProjectId;


                dbContext.SaveChanges();

                var response = new
                {
                    Message = $"Project Employee with ID {id} has been successfully updated."
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/ProjectEmployee
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProjectEmployee([FromRoute] int id)
        {
            var projectEmployee = await dbContext.ProjectEmployee.FindAsync(id);
            if (projectEmployee == null)
            {
                return NotFound();
            }

            dbContext.ProjectEmployee.Remove(projectEmployee);
            await dbContext.SaveChangesAsync();

            return Ok($"ProjectEmployee with ID {id} has been deleted");
        }

    }
}
