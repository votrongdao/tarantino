using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.ListManagement.Impl
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