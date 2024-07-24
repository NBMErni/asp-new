using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO
{
    public class DepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
