using System.Collections.Generic;
using Tarantino.Commons.Core.Services.DataFileManagement.Impl;
using Tarantino.Commons.Core.Services.Environment.Impl;

namespace Tarantino.IntegrationTests.Commons.Core.Services.DataFileManagement
{
	public class LookupRepositoryTester
	{
		protected DataFileReader GetDataFileReader()
		{
			return new DataFileReader(new ResourceFileLocator());
		}

		protected int CountOf<T>(IEnumerable<T> enumerable)
		{
			return new List<T>(enumerable).Count;
		}
	}
}