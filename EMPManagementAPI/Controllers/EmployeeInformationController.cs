using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeInformationController : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public EmployeeInformationController(EMPManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/EmployeeInformation
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employees = dbContext.EmployeeInformation.Include(e => e.Department)
                .Include(e => e.Manager)
               
                .ToList();

            return Ok(employees);
        }

        // GET: api/EmployeeInformation/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = dbContext.EmployeeInformation
                                  .Include(e => e.Manager)
                                  .Include(e => e.Department)
                                  .FirstOrDefault(x => x.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        // POST: api/EmployeeInformation
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeInformationDto employeeInformationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var employee = new EmployeeInformation
                {
                    EmployeeName = employeeInformationDto.EmployeeName,
                    DateOfBirth = employeeInformationDto.DateOfBirth,
                    Gender = employeeInformationDto.Gender,
                    ManagerId = employeeInformationDto.ManagerId,
                    DepartmentId = employeeInformationDto.DepartmentId,
                };


                dbContext.EmployeeInformation.Add(employee);
                dbContext.SaveChanges();

                var response = new
                {
                    Message = $"Employee {employee.EmployeeName} has been successfully created."
                };

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/EmployeeInformation/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeInformationDto employeeInformationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = dbContext.EmployeeInformation.FirstOrDefault(x => x.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            try
            {

                employee.EmployeeName = employeeInformationDto.EmployeeName;
                employee.DateOfBirth = employeeInformationDto.DateOfBirth;
                employee.Gender = employeeInformationDto.Gender;
                employee.ManagerId = employeeInformationDto.ManagerId;
                employee.DepartmentId = employeeInformationDto.DepartmentId;


                dbContext.SaveChanges();
                var response = new
                {
                    Message = $"Employee with ID {employee.EmployeeId} has been successfully updated."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/EmployeeInformation
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await dbContext.EmployeeInformation.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            dbContext.EmployeeInformation.Remove(employee);
            await dbContext.SaveChangesAsync();

            return Ok($"Employee with ID {id} has been deleted");
        }
    }
}