﻿using System.ComponentModel.DataAnnotations;

namespace EMPManagementAPI.Models.DTO.EmployeeInformation
{
    public class CreateEmployeeDto
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int? ManagerId { get; set; }

        [Required]
        public int? DepartmentId { get; set; }


    }
}
