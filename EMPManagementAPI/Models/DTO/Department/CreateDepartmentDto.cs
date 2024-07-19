using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
