using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EMPManagementAPI.Models.Domain
{
    public class ProjectEmployee
    {

        [Key]
        public int ProjectEmployeeId { get; set; }
        
        public int? EmployeeId { get; set; }
       
        public int? ProjectId { get; set; }
        [JsonIgnore]
        public EmployeeInformation EmployeeInformation { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
