using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO;
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
        public IActionResult CreateEmployee([FromBody] ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var project = new Project
                {
                   ProjectName = projectDto.ProjectName,
                   StartDate = projectDto.StartDate,
                   EndDate = projectDto.EndDate,
                   
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
        public IActionResult UpdateProject(int id, [FromBody] ProjectDto projectDto)
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

                project.ProjectName = projectDto.ProjectName;
                project.StartDate = projectDto.StartDate;
                project.EndDate = projectDto.EndDate;

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
