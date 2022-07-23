using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Perscription
    {
        [Key]
        public int PrescriptionId { get; set; }

        public DateTime DateOfPrescription { get; set; }

        public string Prescription { get; set; }

        public string Dosage { get; set; }


        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}