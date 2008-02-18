using System;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISystemClock
	{
		DateTime GetCurrentDateTime();
	}
}