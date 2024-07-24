using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO
{
    public class ProjectEmployeeDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
