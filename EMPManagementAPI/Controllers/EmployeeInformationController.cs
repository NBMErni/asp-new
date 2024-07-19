﻿using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO.EmployeeInformation;
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
            var employees = dbContext.EmployeeInformation.Include(e => e.Department).Include(e => e.Manager).ToList();

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
        public IActionResult CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var employee = new EmployeeInformation
                {
                    EmployeeName = createEmployeeDto.EmployeeName,
                    DateOfBirth = createEmployeeDto.DateOfBirth,
                    Gender = createEmployeeDto.Gender,
                    ManagerId = createEmployeeDto.ManagerId,
                    DepartmentId = createEmployeeDto.DepartmentId,
                };


                dbContext.EmployeeInformation.Add(employee);
                dbContext.SaveChanges();


                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/EmployeeInformation/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
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

                employee.EmployeeName = updateEmployeeDto.EmployeeName;
                employee.DateOfBirth = updateEmployeeDto.DateOfBirth;
                employee.Gender = updateEmployeeDto.Gender;
                employee.ManagerId = updateEmployeeDto.ManagerId;
                employee.DepartmentId = updateEmployeeDto.DepartmentId;


                dbContext.SaveChanges();


                return Ok(employee);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
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

            return Ok(employee);
        }
    }
}