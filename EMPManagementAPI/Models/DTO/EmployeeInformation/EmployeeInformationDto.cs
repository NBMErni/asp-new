using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.EmployeeInformation
{
    public class EmployeeInformationDto
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public int ManagerId { get; set; }

        public int DepartmentId { get; set; }

    }
}
