using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO
{
    public class ManagerDto
    {
        [Required]
        public string ManagerName { get; set; }
    }
}
