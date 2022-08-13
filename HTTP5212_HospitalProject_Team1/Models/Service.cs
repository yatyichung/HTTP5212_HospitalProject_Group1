using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Service
    {
        [Key]
        public int serv_id { get; set; }
        public string serv_name { get; set; }
        public string serv_desc { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
    public class ServiceDto
    {
        [Key]
        public int serv_id { get; set; }
        public string serv_name { get; set; }
        public string serv_desc { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}