using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.Project
{
    public class UpdateProjectDto
    {
        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
    }
}
