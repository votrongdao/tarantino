using System;
using Tarantino.Commons.Core.Model.Enumerations;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
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