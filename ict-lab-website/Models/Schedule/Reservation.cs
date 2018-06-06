using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Reservation
    {
        [Display(Name = "Room name")]
        [RegularExpression("[A-Za-z]+\\.\\d\\.\\d\\d\\d")]
        [Required]
        public string RoomId { get; set; }

        [Display(Name = "From lesson hour")]
        [Range(1,15)]
        [Required]
        public int HourId { get; set; }

        [Display(Name = "Length (in hours)")]
        [Range(1, 15)]
        [Required]
        public int Length { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public string Teacher { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string SpecialReason { get; set; }

        [Required]
        public Boolean Reserved { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
