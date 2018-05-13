using System;
using System.Collections.Generic;
using System.Data;
using ict_lab_website.Models;
using ict_lab_website.Models.Users;

namespace ict_lab_website.Models.ViewModels
{
	public class UsersViewModel
	{

		public DataTable _dataTable { get; }
             
		public int _reservationLimit { get; set; }

		public RoleList _roleList { get; set; }

		public UsersViewModel(DataTable dataTable, RoleList roleList)
        {
            this._dataTable = dataTable;
			this._roleList = roleList;
        }
    }
}
