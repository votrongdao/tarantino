using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.ListManagement
{
	[PluginFamily(Keys.Default)]
	public interface IEnumerationHelper
	{
		IEnumerable<EnumerationType> GetAll<EnumerationType>() where EnumerationType : Enumeration, new();
		int DetermineAbsoluteDifference(Enumeration firstValue, Enumeration secondValue);
	}
}