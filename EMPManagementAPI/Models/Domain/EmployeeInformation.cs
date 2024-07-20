using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EMPManagementAPI.Models.Domain
{
    public class EmployeeInformation
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; }

        //Prints only the ID of the selected variable
        
        public int? ManagerId { get; set; }
        
        public int? DepartmentId { get; set; }

        // Navigation property 
        //Prints out the data from the ID that is selected
        [JsonIgnore]
        public Manager Manager { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
