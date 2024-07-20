using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO.EmployeeInformation;
using EMPManagementAPI.Models.DTO.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public ProjectController(EMPManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Project
        [HttpGet]
        public IActionResult GetAllProject()
        {
            var projects = dbContext.Project.ToList();

            return Ok(projects);
        }

        // GET: api/Project/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetProjectById(int id)
        {
            var project = dbContext.Project.FirstOrDefault(x => x.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] CreateProjectDto createProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var project = new Project
                {
                   ProjectName = createProjectDto.ProjectName,
                   StartDate = createProjectDto.StartDate,
                   EndDate = createProjectDto.EndDate,
                   
                };


                dbContext.Project.Add(project);
                dbContext.SaveChanges();


                return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Project/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = dbContext.Project.FirstOrDefault(x => x.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            try
            {

                project.ProjectName = updateProjectDto.ProjectName;
                project.StartDate = updateProjectDto.StartDate;
                project.EndDate = updateProjectDto.EndDate;

                dbContext.SaveChanges();


                return Ok(project);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Project
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            var project = await dbContext.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            dbContext.Project.Remove(project);
            await dbContext.SaveChangesAsync();

            return Ok($"Project with ID {id} has been deleted");
        }

    }
}
