using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.ProjectEmployee
{
    public class CreateProjectEmployee
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

    }
}