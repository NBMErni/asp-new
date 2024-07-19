﻿using EMPManagementAPI.Models;
using EMPManagementAPI.Models.Domain;
using EMPManagementAPI.Models.DTO.EmployeeInformation;
using EMPManagementAPI.Models.DTO.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMPManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly EMPManagementDbContext dbContext;

        public ManagerController(EMPManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        // GET: api/manager
        [HttpGet]
        public IActionResult GetAllManagers()
        {
            var managers = dbContext.Manager.ToList();

            return Ok(managers);
        }

        // GET: api/Manager/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetManagerById([FromRoute] int id)
        {
            var manager = dbContext.Manager.FirstOrDefault(x => x.ManagerId == id);

            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] CreateManagerDto createManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var manager = new Manager
                {
                    ManagerName = createManagerDto.ManagerName
                };

                dbContext.Manager.Add(manager);
                dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetManagerById), new { id = manager.ManagerId }, manager);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Manager/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateManager(int id, [FromBody] UpdateManagerDto updateManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manager = dbContext.Manager.FirstOrDefault(x => x.ManagerId == id);

            if (manager == null)
            {
                return NotFound();
            }

            try
            {
                manager.ManagerName = updateManagerDto.DepartmentName;

                dbContext.SaveChanges();

                return Ok(manager);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteManager([FromRoute] int id)
        {
            var manager = await dbContext.Manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            dbContext.Manager.Remove(manager);
            await dbContext.SaveChangesAsync();

            return Ok(manager);
        }
    }
}