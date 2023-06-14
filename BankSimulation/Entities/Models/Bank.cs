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
		public DateTime OriginHistiry { get;  }
        public List<int> Rates { get; set; }
        public List<User> Users { get; set; }

		public Bank()
		{
			Rates = new List<int>();
			Users = new List<User>();
            OriginHistiry = DateTime.Now;
		}
	}
}

