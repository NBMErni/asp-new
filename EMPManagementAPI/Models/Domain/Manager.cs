using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.Domain
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }

        public string ManagerName { get; set; }
    }
}
