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
        public string RoomName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "From lesson hour")]
        [Range(1,15)]
        [Required]
        public int StartLessonHour { get; set; }

        [Display(Name = "Until lesson hour")]
        [Range(1,15)]
        [Required]
        public int EndLessonHour { get; set; }

        [Required]
        public string Reserver { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}
