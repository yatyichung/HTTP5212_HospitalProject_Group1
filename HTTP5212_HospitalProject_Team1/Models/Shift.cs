using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Shift
    {
        [Key]
        public int ShiftID { get; set; }

        public string ShiftTime { get; set; }

        public Boolean ShiftSun { get; set; }

        public Boolean ShiftMon { get; set; }

        public Boolean ShiftTues { get; set; }

        public Boolean ShiftWed { get; set; }

        public Boolean ShiftThurs { get; set; }

        public Boolean ShiftFri { get; set; }

        public Boolean ShiftSat { get; set; }

        //each shift belong to one employee
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

    }
    public class ShiftDto
    {
        public int ShiftID { get; set; }

        public string ShiftTime { get; set; }

        public Boolean ShiftSun { get; set; }

        public Boolean ShiftMon { get; set; }

        public Boolean ShiftTues { get; set; }

        public Boolean ShiftWed { get; set; }

        public Boolean ShiftThurs { get; set; }

        public Boolean ShiftFri { get; set; }

        public Boolean ShiftSat { get; set; }

        public string EmployeeLastName { get; set; }


    }
}