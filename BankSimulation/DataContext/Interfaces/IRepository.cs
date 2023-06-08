using System;
using System.Collections.Generic;
using Entities.Interfaces;

namespace DataContext.Interfaces
{
	public interface IRepository<T>where T:IEntity
	{
		public bool Create(T entity);
		public bool Delete(T entity);
		public bool Update(T entity);
		public List<T> GetAll(Predicate<T> filter=null);
		public T Get(Predicate<T> filter);

	}
}

