using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
