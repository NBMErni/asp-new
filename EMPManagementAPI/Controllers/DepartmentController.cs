using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO.Department;
using EMPManagementAPI.Models.DTO.EmployeeInformation;
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
        public IActionResult CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var department = new Department
                {
                   DepartmentName = createDepartmentDto.DepartmentName
                };


                dbContext.Department.Add(department);
                dbContext.SaveChanges();


                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, department);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Department/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
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
                department.DepartmentName = updateDepartmentDto.DepartmentName;

                dbContext.SaveChanges();

                return Ok(department);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

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

            return Ok(department);
        }
    }
}
