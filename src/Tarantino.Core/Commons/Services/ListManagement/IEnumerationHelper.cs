using System.Collections.Generic;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model.Enumerations;
using StructureMap;

namespace Tarantino.Commons.Core.Services.ListManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IEnumerationHelper
	{
		IEnumerable<EnumerationType> GetAll<EnumerationType>() where EnumerationType : Enumeration, new();
		int DetermineAbsoluteDifference(Enumeration firstValue, Enumeration secondValue);
	}
}