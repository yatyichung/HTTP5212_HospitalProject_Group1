using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HTTP5212_HospitalProject_Team1.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public string RoomType { get; set; }

        public int BlockFloor { get; set; }

        public bool Availability { get; set; }
    }
}