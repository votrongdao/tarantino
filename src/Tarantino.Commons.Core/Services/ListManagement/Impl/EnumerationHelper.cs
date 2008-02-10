using System.Collections.Generic;
using Tarantino.Commons.Core.Model.Enumerations;
using StructureMap;

namespace Tarantino.Commons.Core.Services.ListManagement.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class EnumerationHelper : IEnumerationHelper
	{
		public IEnumerable<EnumerationType> GetAll<EnumerationType>() where EnumerationType : Enumeration, new()
		{
			return Enumeration.GetAll<EnumerationType>();
		}

		public int DetermineAbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
		{
			return Enumeration.AbsoluteDifference(firstValue, secondValue);
		}
	}
}