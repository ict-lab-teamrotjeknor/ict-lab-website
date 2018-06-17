using System;
namespace ict_lab_website.Models.Users
{
    public class ChangeReservationLimit
    {
		public string UserEmail { get; set; }
		public int reservationlimit { get; set; }

		public ChangeReservationLimit(string _email, int _reservationlimit)
		{
			UserEmail = _email;
			reservationlimit = _reservationlimit;
		}
    }
}
 