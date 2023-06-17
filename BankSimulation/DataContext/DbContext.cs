using System;
using System.Collections.Generic;
using Entities.Models;

namespace DataContext
{
	public static class DbContext
	{
		public static List<User> Users { get; set; }
		public static List<Bank> Banks { get; set; }
		public static List<Bank> DeletingBanks { get; set; }
		public static List<User> BlockedUsers { get; set; }
		static DbContext()
		{
			Users = new List<User>();
			Banks = new List<Bank>();
			DeletingBanks = new List<Bank>();
			BlockedUsers = new List<User>();
		}
	}
}

