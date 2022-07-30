using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5212_HospitalProject_Team1.Models.ViewModels
{
    public class UpdatePaySlip
    {
        public PaySlipDto SelectedPaySlip { get; set; }

        //all employees to choose from when updating this shift
        public IEnumerable<EmployeeDto> EmployeesOptions { get; set; }
    }
}