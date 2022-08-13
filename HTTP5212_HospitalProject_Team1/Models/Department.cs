﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Department
    {
        [Key]
        public int dept_id { get; set; }
        public string dept_name { get; set; }
        public string dept_desc { get; set; }
        public int serv_id { get; set; }
        public string serv_name { get; set; }
    }
    public class DepartmentDto
    {
        [Key]
        public int dept_id { get; set; }
        public string dept_name { get; set; }
        public string dept_desc { get; set; }
        public int serv_id { get; set; }
        public string serv_name { get; set; }
    }
}