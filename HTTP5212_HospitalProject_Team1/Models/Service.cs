using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Service
    {
        [Key]
        public int serv_id { get; set; }
        public string serv_name { get; set; }
        public string serv_desc { get; set; }
    }
}