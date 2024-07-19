namespace EMPManagementAPI.Models.Domain
{
    public class ProjectEmployee
    {
        public int ProjectEmployeeId { get; set; }

        public int? EmployeeId { get; set; }

        public int? ProjectId { get; set; }

        public EmployeeInformation EmployeeInformation { get; set; }

        public Project Project { get; set; }
    }
}
