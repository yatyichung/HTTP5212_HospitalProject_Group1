using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //---NO NEED FOREIGN KEY
        //one employee can have multiple appointment (appointment.cs have employee as foreign key)
        //one employee can have multiple payslip (payslip.cs have employee as foreign key)
        //one employee can have one shift (foreign key is already made in shift.cs)


        //multiple employee can be responsible for the same patient (require Patient as foreign key)
        //[ForeignKey("Patient")]
        //public int PatientId { get; set; }
        //public virtual Patient Patient { get; set; }
    }
}