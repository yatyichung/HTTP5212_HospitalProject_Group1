using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmployeeRole { get; set; }

        public DateTime EmployeeJoinDate { get; set; }
    }
}