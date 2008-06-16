using StructureMap;
using Tarantino.Core.Commons.Services.Configuration;

namespace Tarantino.Infrastructure.Commons.Services.Configuration
{
	public class ServiceLocator : IServiceLocator
	{
		public T CreateInstance<T>()
		{
			var instance = ObjectFactory.GetInstance<T>();
			return instance;
		}

		public T CreateInstance<T>(string instanceKey)
		{
			var instance = ObjectFactory.GetNamedInstance<T>(instanceKey);
			return instance;
		}
	}
}