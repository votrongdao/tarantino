using System;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISystemClock
	{
		DateTime GetCurrentDateTime();
	}
}