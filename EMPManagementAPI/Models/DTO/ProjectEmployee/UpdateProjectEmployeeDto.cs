using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.ProjectEmployee
{
    public class UpdateProjectEmployeeDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
