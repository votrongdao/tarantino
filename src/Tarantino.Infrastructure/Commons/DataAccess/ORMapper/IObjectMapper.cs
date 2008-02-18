using System;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IObjectMapper
	{
		void Save(object domainObject);
		void SaveOrUpdate(object domainObject);
		void Delete(params object[] domainObjects);
		T Load<T>(object id);
		void Flush();
		void Evict(params object[] domainObjects);
		void Refresh(params object[] domainObjects);
		object Run(SessionCommand command, bool isTransaction, params object[] arguments);
		T[] LoadAll<T>();
		void ExecuteNonQuery(string sql, Type type);
		bool IsDirty();
		void Lock(object domainObject);
		void Add(params object[] domainObjects);
		void AddNew(object domainObject);
		object[] LoadAll(Type type);
	}
}