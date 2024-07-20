using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.Domain
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

       
    }
}
