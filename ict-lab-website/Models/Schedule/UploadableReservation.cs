using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    // This class matches the JSON object that is required by the endpoint "/schedule/uploadhour" of the API.
    // Quator means something like 'quarter', or 'period' ..I guess. 
    public class UploadableReservation
    {
        public string Username { get; set; }
        public string Type { get; set; }

        [Display(Name = "Room name")]
        [RegularExpression("[A-Za-z]+\\.\\d\\.\\d\\d\\d")]
        [Required]
        public string Classroom { get; set; }

        [Required]
        public int Year { get; set; }

        [Display(Name = "School period")]
        [Range(1, 4)]
        [Required]
        public int Quator { get; set; }

        [Display(Name = "Week (1 - 52)")]
        [Range(1, 52)]
        [Required]
        public int Week { get; set; }

        [Required]
        public string Day { get; set; }

        [Display(Name = "Start lesson hour")]
        [Range(1, 15)]
        [Required]
        public int StartHour { get; set; }

        [Display(Name = "Amount of hours to reserve")]
        [Range(1, 15)]
        [Required]
        public int TotalHours { get; set; }
    }
}
