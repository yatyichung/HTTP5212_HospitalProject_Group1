using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string TypeOfAppointment { get; set; }

        public DateTime AppointmentTime { get; set; }

       // To Do
       // Employee
       // patient
    }
}