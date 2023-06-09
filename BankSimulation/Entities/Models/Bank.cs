using System;
using System.Collections.Generic;
using Entities.Interfaces;

namespace Entities.Models
{
	public class Bank:IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Signature { get; set; }
		private DateTime OriginHistiry { get; set; }
		public List<User> Users { get; set; }

		public Bank()
		{
			Users = new List<User>();
            OriginHistiry = DateTime.Now;
		}
	}
}

