using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.Domain
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
