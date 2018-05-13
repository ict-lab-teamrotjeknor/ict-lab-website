using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Process;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


namespace ict_lab_website.Models.Users
{
    public class ChangeRole
    {
		public int Value { get; set; }
		public string Text { get; set; }
		public bool IsChecked { get; set; }
    }
    
	public class RoleList
	{
		public List<ChangeRole> Roles { get; set;}
	}
}
