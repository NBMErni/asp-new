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
    public class DepartmentController : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public DepartmentController(EMPManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Department
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = dbContext.Department.ToList();

            return Ok(departments);
        }

        // GET: api/EmployeeInformation/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetDepartmentById(int id)
        {
            var department = dbContext.Department.FirstOrDefault(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // POST: api/EmployeeInformation
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var department = new Department
                {
                   DepartmentName = departmentDto.DepartmentName
                };


                dbContext.Department.Add(department);
                dbContext.SaveChanges();

                  var response = new
                {
                    Message = $"Department {department.DepartmentName} has been successfully added."
                };

                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Department/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = dbContext.Department.FirstOrDefault(x => x.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            try
            {
                department.DepartmentName = departmentDto.DepartmentName;

                dbContext.SaveChanges();

                var response = new
                {
                    Message = $"Department with ID {id} has been successfully updated."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Department
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var department = await dbContext.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            dbContext.Department.Remove(department);
            await dbContext.SaveChangesAsync();

            return Ok($"Department with ID {id} has been deleted");
        }
    }
}
