using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.Domain
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

       
    }
}
