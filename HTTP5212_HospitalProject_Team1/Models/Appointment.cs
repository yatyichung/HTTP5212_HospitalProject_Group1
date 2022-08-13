using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string TypeOfAppointment { get; set; }

        public DateTime AppointmentTime { get; set; }


        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
    public class AppointmentDto
    {
        [Key]
        public int AppointmentId { get; set; }

        public string TypeOfAppointment { get; set; }

        public DateTime AppointmentTime { get; set; }


        public int PatientID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}