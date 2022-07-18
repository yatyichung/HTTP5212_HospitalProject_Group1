using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class PaySlip
    {
        public int PaySlipID { get; set; }

        public int PaySlipHoursWorked { get; set; }

        public int PaySlipSinNum { get; set; }  

        public int PaySlipHourlyWage { get; set; }

        public DateTime PaySlipPaymentDate{ get; set; }

        /*
         TODO: employee
         */
}
}