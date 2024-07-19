using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.Manager
{
    public class CreateManagerDto
    {
        [Required]
        public string ManagerName { get; set; }
    }
}
