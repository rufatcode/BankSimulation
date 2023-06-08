using System;
using System.Collections.Generic;
using Entities.Models;

namespace DataContext
{
	public static class DbContext
	{
		public static List<User> Users { get; set; }
		public static List<Bank> Banks { get; set; }
		static DbContext()
		{
			Users = new List<User>();
			Banks = new List<Bank>();
		}
	}
}

