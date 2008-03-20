using System;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface IDateContext
	{
		int GetCurrentYear();
		DateTime GetCurrentDate();
		DateTime GetFirstDayOfCurrentMonth();
		DateTime GetFirstDayOfCurrentYear();
		DateTime GetFirstDayOfTimePeriod(TimePeriod timePeriod);
		string GetTimePeriodName(TimePeriod timePeriod);
	}
}