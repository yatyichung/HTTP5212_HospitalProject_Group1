﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5212_HospitalProject_Team1.Models.ViewModels
{
    public class UpdateService
    {
        public ServiceDto selectedservice { get; set; }

        public IEnumerable<DepartmentDto> DeptOptions { get; set; }
    }
}