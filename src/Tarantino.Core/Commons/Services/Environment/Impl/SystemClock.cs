using System;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class SystemClock : ISystemClock
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}