using System;
using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationInstanceCache
	{
		ApplicationInstance GetCurrent();
		void Set(string key, ApplicationInstance item);
	}
}