using System;
using System.Collections.Generic;
using System.Data;
using ict_lab_website.Models;
namespace ict_lab_website.Models.ViewModels
{
	public class UsersViewModel
	{
		public enum Role
        {
            Guest = 1,
            Student = 2,
            Teacher = 3,
            Handyman = 4,
            Servicedesk = 5,
            Rastermaker = 6,
            Administrator = 7
        }

		public DataTable _dataTable { get; }
        
        public int _reservationLimit { get; set; }
       
		public Role _role { get; set; }

        public UsersViewModel(DataTable dataTable)
        {
            this._dataTable = dataTable;
        }
    }
}
