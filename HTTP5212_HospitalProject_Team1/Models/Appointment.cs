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
}