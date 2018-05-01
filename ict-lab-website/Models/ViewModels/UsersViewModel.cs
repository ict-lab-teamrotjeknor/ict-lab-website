using System;
using System.Collections.Generic;
using System.Data;
using ict_lab_website.Models;
namespace ict_lab_website.Models.ViewModels
{
    public class UsersViewModel
    {
        public DataTable _dataTable { get; }

        public UsersViewModel(DataTable dataTable)
        {
            this._dataTable = dataTable;
        }
    }
}
