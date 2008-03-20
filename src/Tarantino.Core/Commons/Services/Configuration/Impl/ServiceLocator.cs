using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	[Pluggable(Keys.Default)]
	public class ServiceLocator : IServiceLocator
	{
		public T CreateInstance<T>()
		{
			T instance = ObjectFactory.GetInstance<T>();
			return instance;
		}

		public T CreateInstance<T>(string instanceKey)
		{
			T instance = ObjectFactory.GetNamedInstance<T>(instanceKey);
			return instance;
		}
	}
}