﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5212_HospitalProject_Team1.Models.ViewModels
{
    public class UpdateDepartment
    {
        public DepartmentDto selecteddepartment { get; set; }

        public IEnumerable<ServiceDto> ServOptions { get; set; }
    }
}