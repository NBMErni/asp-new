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

        public int? ManagerId { get; set; }

        public int? DepartmentId { get; set; }

        // Navigation property 
        [JsonIgnore]
        public Manager Manager { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
