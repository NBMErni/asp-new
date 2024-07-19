using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.Manager
{
    public class UpdateManagerDto
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
