using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RemoteEmployeeService.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Column("EmployeeId")]
        [MaxLength(10)]
        public string EmployeeId { get; set; }


        [Column("Password")]
        [MaxLength(20)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string EmployeeName { get; set; }
        public int Age { get; set; }
    }
}