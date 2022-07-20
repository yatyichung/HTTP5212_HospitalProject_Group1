using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientAddress { get; set; }

        public int PatientRoomNumber { get; set; }
    }
}