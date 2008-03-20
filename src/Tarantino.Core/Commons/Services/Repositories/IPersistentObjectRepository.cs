using System;
using System.Collections.Generic;
using Tarantino.Core.Commons.Model;
using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Repositories
{
	[PluginFamily(Keys.Default)]
	public interface IPersistentObjectRepository
	{
		IEnumerable<T> GetAll<T>();
		T GetById<T>(Guid id) where T : PersistentObject;
		void Save(PersistentObject persistentObject);
		void Revert(PersistentObject persistentObject);
		void Delete(PersistentObject persistentObject);
		IEnumerable<T> FindAll<T>(CriterionSet criterionSet);
		T FindFirst<T>(CriterionSet set) where T : class;

		string ConnectionStringKey
		{
			get;
			set;
		}
	}
}