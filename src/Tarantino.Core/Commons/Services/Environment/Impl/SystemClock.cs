using System;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(Keys.Default)]
	public class SystemClock : ISystemClock
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}